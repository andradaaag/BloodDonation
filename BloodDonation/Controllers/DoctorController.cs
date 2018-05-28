using BloodDonation.Logic.Services;
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

            return View("DoctorShowRequestsView",requests);
        }

        public ActionResult GetMakeBloodRequest()
        {
            return View("MakeRequestView");
        }


        public ActionResult DeleteRequest(BloodDonation.Models.RequestPersonnel info)
        {
            requestService.DeleteById(info.ID);
            System.Threading.Thread.Sleep(700);
            return MainDoctorPage();
        }

        [HttpPost]
        public ActionResult CreateRequest(RequestBloodForm request)
        {
            Regex regex = new Regex("^[0-9]{13}$");
            if(!regex.IsMatch(request.patientCnp))
            {
                //// TODO mesaj de eroare somehow
                return GetMakeBloodRequest();
            }

            Logic.Models.RequestPersonnel newRequest = 
                businessToPresentationMapperDoctor.MapRequestBloodFormToRequestPersonnel(request, GetUid());
            requestService.AddRequest(newRequest);

            System.Threading.Thread.Sleep(700);
            return MainDoctorPage();
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