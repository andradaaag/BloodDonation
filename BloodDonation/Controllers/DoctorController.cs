using BloodDonation.Logic.Services;
using BloodDonation.Mappers;
using BloodDonation.Logic.Models;

using System;
using System.Web.Mvc;

using System.Collections.Generic;

using System.Linq;
using BloodDonation.Models;
using Firebase.Auth;

namespace BloodDonation.Controllers
{
    public class DoctorController : Controller
    {
        private BusinessToPresentationMapperDoctor businessToPresentationMapperDoctor = new BusinessToPresentationMapperDoctor();
        private BusinessToPresentationMapperPersonnel businessToPresentationMapperPersonnel = new BusinessToPresentationMapperPersonnel();

        private PresentationToBusinessMapperDoctor presentationToBusinessMapperDoctor = new PresentationToBusinessMapperDoctor();
        private PresentationToBusinessMapperPersonnel presentationToBusinessMapperPersonnel = new PresentationToBusinessMapperPersonnel();

        private RequestService requestService = new RequestService();

        List<Models.RequestPersonnel> requests;

        public ActionResult Index()
        {
            return MainDoctorPage();
        }

        public ActionResult MainDoctorPage()
        {

            requests = requestService
                        .FindByDoctorId(GetUid())
                        .AsEnumerable()
                        .Select(el => businessToPresentationMapperPersonnel.Request(el))
                        .ToList();

            return View("DoctorShowRequestsView",
                            requests
                        );
        }

        public ActionResult GetMakeBloodRequest()
        {
            return View("MakeRequestView");
        }


        [HttpPost]
        public ActionResult CreateAdmin(CreateAdminForm form)
        {
            MailMessage message = new System.Net.Mail.MailMessage();
            message.From = new MailAddress("blooddonationiss@gmail.com", "Blood Donation App");
            message.To.Add(new MailAddress(form.EmailAddress));
            message.Subject = "this is a test email.";
            message.Priority = MailPriority.High;
            message.IsBodyHtml = false;
            message.Sender = new MailAddress("blooddonationiss@gmail.com", "Sender Name");
            message.Body = "this is my test email body for " + form.FirstName + " " + form.LastName;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new System.Net.NetworkCredential("blooddonationiss@gmail.com", "bdisspass8");

            smtp.Send(message);

            return GetAdminCreatedPage();
        }


        public String GetUid()
        {
            try
            {
                return ((FirebaseAuthLink)Session["authlink"]).User.LocalId;

            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}