using System.Collections.Generic;
using System.Web.Mvc;
using BloodDonation.Business.Services;
using BloodDonation.Logic.Models;
using BloodDonation.Mappers;
using BloodDonation.Models;

namespace BloodDonation.Controllers
{
    public class DonorController : Controller
    {
        private readonly BusinessToPresentationMapper
            _businessToPresentationMapper = new BusinessToPresentationMapper();

        private readonly DonorService donorService = new DonorService();

        public ActionResult Index()
        {
            List<DonationDetails> donationDetails = donorService.GetDonationDetails();
            
            ShowDonorDonations details = new ShowDonorDonations();
            foreach (DonationDetails detail in donationDetails)
            {
                details.AddDonationDetails(_businessToPresentationMapper.MapDonorDonationDetails(detail));
            }

            return View("ShowDonorDonationsView", details);
        }
    }
}