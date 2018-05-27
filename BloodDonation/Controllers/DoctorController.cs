using BloodDonation.Logic.Services;
using BloodDonation.Mappers;
using BloodDonation.Logic.Models;

using System;
using System.Web.Mvc;

using System.Collections.Generic;

using System.Linq;
using BloodDonation.Models;

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

        public String GetUid()
        {
            try
            {
                return (String)Session["localId"];
            }catch(Exception e)
            {
                return "";
            }
        }
    }
}