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

        private DoctorService doctorService = new DoctorService();
        private HospitalService hosp = new HospitalService();

        private RequestService requestService = new RequestService();

        List<Models.RequestPersonnel> requests;

        public ActionResult Index()
        {
            //List<HospitalTransferObject> hto = hosp.GetAllHospitals();
            HospitalTransferObject hto1 = hosp.GetHospitalById("-LDWUaTBY-N7EzgxNtZ4");
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

        [HttpPost]
        public ActionResult CreateRequest(RequestBloodForm request)
        {
            Regex regex = new Regex("^[0-9]{13}$");
            if(!regex.IsMatch(request.patientCnp))
            {
                return GetMakeBloodRequest();
            }

            Logic.Models.RequestPersonnel newRequest = new Logic.Models.RequestPersonnel();
            newRequest.bloodType.Group = request.bloodGroup;
            newRequest.bloodType.RH = request.bloodRh == "positive" ? true : false;
            newRequest.patientCnp = request.patientCnp;
            newRequest.quantity = request.quantity;
            newRequest.status = Logic.Models.Status.BeingProcessed;
            newRequest.doctorId = GetUid();

            Doctor doctor = doctorService.findById(newRequest.doctorId);
            if(doctor == null)
            {
                return GetMakeBloodRequest();
            }
            newRequest.destination = doctor.HospitalId;

            newRequest.source = "none";
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