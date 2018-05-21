﻿
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
        private readonly BusinessToPresentationMapperDonor
            _businessToPresentationMapper = new BusinessToPresentationMapperDonor();
        
        private readonly PresentationToBusinessMapperDonor _presentationToBusinessMapperDonor = new PresentationToBusinessMapperDonor();

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

        public ActionResult DonorPersonalDetailsView()
        {
            DonorDetailsTransferObject donorDetailsTransferObject = donorService.GetDonorDetailsById("id");
            DonorAccountRequest donorAccountRequest =
                _businessToPresentationMapper.MapDonorAccountRequest(donorDetailsTransferObject);
            
            return View("DonorPersonalDetailsView", donorAccountRequest);
        }
        
        public ActionResult GetEditDonorPersonalDataPage()
        {
            return View("EditDonorPersonalDataView", new DonorAccountRequest());
        }

        public ActionResult BookDonationView()
        {
            return View("BookDonationView", new DonorAccountRequest());
        }
        
        [HttpPost]
        public ActionResult UpdateDonorPersonalData(DonorAccountRequest formDetails)
        {
            // TODO - somehow update personal data
            formDetails.ID = "id";
            
            donorService.EditDonorDetails(_presentationToBusinessMapperDonor.MapDonorForm(formDetails));
            return DonorPersonalDetailsView();
        }
    }  
}