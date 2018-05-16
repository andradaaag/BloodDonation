using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class PersonnelController : Controller
    {

        private DonationModel donationModel;
        // GET: Personnel
        public ActionResult Index()
        {
            return View("AddDonationView");
        }

        public ActionResult AddDonation()
        {
            donationModel = new DonationModel();
            donationModel.BloodType = new Data.Models.BloodType();
            
            return View("AddDonationView",donationModel);
        }

        //[HttpPost]
        public ActionResult WooAddDonation(DonationModel donation)
        {
            Console.WriteLine("WOOOOO");
            //TODO: for some reason I get an empty donation
            return View("AddDonationView");
        }
    }
}