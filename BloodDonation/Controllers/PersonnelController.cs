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
            donationModel = new DonationModel
            {
                BloodType = new BloodType()
            };

            return View("AddDonationView",donationModel);
        }

        [HttpPost]
        public ActionResult AddDonationInDb(DonationModel donation)
        {
            //TODO : add the database part (aka: actually do something)
            return Index();
        }
        public ActionResult Test()
        {
            return View("Test");
        }
    }
}