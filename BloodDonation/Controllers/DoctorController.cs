using BloodDonation.Logic.Services;
using BloodDonation.Mappers;
using BloodDonation.Logic.Models;
using Firebase.Auth;
using System;
using System.Collections.Generic;
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
            return View("DoctorShowRequestsView", 
                        requestService
                            .FindByDoctorId
                                   (
                                        this.GetUid()
                                    )
                        );
        }

        public String GetUid()
        {
            try
            {
                return ((FirebaseAuthLink)Session["authlink"]).User.LocalId;
            }catch(Exception e)
            {
                Console.Beep();
                Console.Beep();
                Console.Beep();
                Console.Beep();
                Console.Beep();
                return "";
            }
        }
    }
}