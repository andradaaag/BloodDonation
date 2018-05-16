using BloodDonation.Logic.Services;
using BloodDonation.Mappers;
using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class PersonnelController : Controller
    {

        

        private DonationService donationService = new DonationService();

        private BusinessToPresentationMapperPersonnel BusinessToPresentation = new BusinessToPresentationMapperPersonnel();
        private PresentationToBusinessMapperPersonnel PresentationToBusiness = new PresentationToBusinessMapperPersonnel();

        public ActionResult Index()
        {
            return View("AddDonationView");
        }

        public ActionResult AddDonation()
        {
            return View("AddDonationView");
        }

        [HttpPost]
        public async Task<ActionResult> AddDonationInDb(DonationModel donation)
        {
            donation.Stage = "Sampling";
            //TODO: add donation time EVERYWHERE

            await donationService.Add(PresentationToBusiness.Donation(donation));
            
            return Index();
        }

        public ActionResult EditDonationSeparation(DonationModel donation)
        {
            return View("EditDonationSeparation", donation);
        }

        [HttpPost]
        public ActionResult SeparateComponentsToDB(DonationModel donation)
        {
            //TODO : add the database part (aka: actually do something)
            return Index();
        }

    }
}