using BloodDonation.Logic.Models;
using BloodDonation.Logic.Services;
using BloodDonation.Mappers;
using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class DoctorController : Controller
    {
        private readonly BusinessToPresentationMapperDoctor businessToPresentationMapperDoctor = new BusinessToPresentationMapperDoctor();
        private readonly PresentationToBusinessMapperDoctor presentationToBusinessMapperDoctor = new PresentationToBusinessMapperDoctor();

        private readonly RequestService requestService = new RequestService();

        public ActionResult Index()
        {
            return MainDoctorPage();
        }

        public ActionResult MainDoctorPage()
        {
            requestService.FindByDoctorId();


            return View("DoctorShowRequestsView", elems);
        }

        public 
    }
}