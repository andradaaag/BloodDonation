﻿using BloodDonation.Logic.Services;
using BloodDonation.Mappers;
using BloodDonation.Logic.Models;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using BloodDonation.Models;
using Firebase.Auth;
using System.Text.RegularExpressions;
using BloodDonation.Data.Models;
using System.Net.Mail;
using System.Threading;
using BloodDonation.Services;
using RequestService = BloodDonation.Logic.Services.RequestService;

namespace BloodDonation.Controllers
{
    public class DoctorController : Controller
    {
        private BusinessToPresentationMapperDoctor businessToPresentationMapperDoctor =
            new BusinessToPresentationMapperDoctor();

        private BusinessToPresentationMapperPersonnel businessToPresentationMapperPersonnel =
            new BusinessToPresentationMapperPersonnel();

        private PresentationToBusinessMapperDoctor presentationToBusinessMapperDoctor =
            new PresentationToBusinessMapperDoctor();

        private PresentationToBusinessMapperPersonnel presentationToBusinessMapperPersonnel =
            new PresentationToBusinessMapperPersonnel();

        private RequestService requestService = new RequestService();
        private DoctorService doctorService = new DoctorService();
        private DonationService donationService = new DonationService();
        private DoctorServicePresentation doctorServicePresentation = new DoctorServicePresentation();

        List<Models.RequestPersonnel> requests;

        private ActionResult goIfPossible(ActionResult actionResultSuccess)
        {
            Thread.Sleep(1000);

            if (Session["usertype"] == null)
                return RedirectToAction("Index", "Login");
            if ((string)Session["usertype"] != "doctor")
                return RedirectToAction("Error", "Error");
            if (Session["authlink"] != null && ((FirebaseAuthLink)Session["authlink"]).IsExpired())
                return RedirectToAction("Index", "Login");

            return actionResultSuccess;
        }

        public ActionResult Index()
        {
            return goIfPossible(MainDoctorPage());
        }

        public ActionResult PatientDonations()
        {
            var statuses = doctorServicePresentation.GetDonationStatusesForDoctor(GetUid());
            return goIfPossible(View("PatientDonationsView", statuses));
        }

        public ActionResult MainDoctorPage()
        {
            requests = requestService
                .FindByDoctorId(GetUid())
                .AsEnumerable()
                .Select(el => businessToPresentationMapperPersonnel.Request(el))
                .ToList();

            return goIfPossible(View("DoctorShowRequestsView", requests));
        }

        public ActionResult GetMakeBloodRequest()
        {
            return goIfPossible(View("MakeRequestView"));
        }


        public ActionResult DeleteRequest(BloodDonation.Models.RequestPersonnel info)
        {
            requestService.DeleteById(info.ID);
            System.Threading.Thread.Sleep(700);
            return goIfPossible(MainDoctorPage());
        }

        public ActionResult CompleteRequest(BloodDonation.Models.RequestPersonnel info)
        {
            BloodDonation.Logic.Models.RequestPersonnel request = requestService.GetOne(info.ID);

            String patientCnp = request.patientCnp;
            doctorService.RemoveCnp(patientCnp);

            request.status = Utils.Enums.Status.Completed;
            requestService.Edit(request);
            System.Threading.Thread.Sleep(700);
            return goIfPossible(MainDoctorPage());
        }


        private void SendBloodRequestMail(Logic.Models.RequestPersonnel newrequest, String destinationEmail)
        {
            EmailServiceBloodDonation emailService = new EmailServiceBloodDonation();
            HospitalService hospitalService = new HospitalService();

            Doctor doctorInfo = doctorService.findById(newrequest.doctorId);
            HospitalTransferObject hospital = hospitalService.GetHospitalById(newrequest.destination);
            if (doctorInfo != null)
            {
                String text = "A blood request has been made by " + "Dr. " + doctorInfo.firstName + " " +
                              doctorInfo.lastName + "\n";
                text += "The required blood is :\n\tGroup:\t" + newrequest.bloodType.Group + "\n\tRh:\t" +
                        (newrequest.bloodType.RH == true ? "POSITIVE" : "NEGATIVE") + "\n\tQuantity:\t" +
                        newrequest.quantity + "\n";
                text += "The hospital where the blood is needed:\n\tName:\t" + hospital.Name + "\n\tLocation:\t" +
                        hospital.Location + "\n";
                text += "Please check the 'BloodDonation' application";

                String title = "Blood Request At " + hospital.Name + " Hospital In " + hospital.Location;


                emailService.SendEmail(text, title, doctorInfo.firstName + " " + doctorInfo.lastName, destinationEmail);
            }
        }

        private void sendEmailToAllPersonnel(Logic.Models.RequestPersonnel newrequest)
        {
            PersonnelService personnelService = new PersonnelService();
            personnelService.FindAll()
                .ForEach(staff => SendBloodRequestMail(newrequest, staff.emailAddress));
        }


        [HttpPost]
        public ActionResult CreateRequest(RequestBloodForm request)
        {
            Regex regex = new Regex("^[0-9]{13}$");
            if (!regex.IsMatch(request.patientCnp))
            {
                //// TODO mesaj de eroare somehow
                return GetMakeBloodRequest();
            }

            Logic.Models.RequestPersonnel newRequest =
                businessToPresentationMapperDoctor.MapRequestBloodFormToRequestPersonnel(request, GetUid());
            requestService.AddRequest(newRequest);

            Thread mailThread = new Thread(() => sendEmailToAllPersonnel(newRequest));
            mailThread.Start();

            System.Threading.Thread.Sleep(700);
            return goIfPossible(MainDoctorPage());
        }

        public String GetUid()
        {
            try
            {
                return ((FirebaseAuthLink) Session["authlink"]).User.LocalId;
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}