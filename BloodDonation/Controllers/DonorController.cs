
using System.Collections.Generic;
using System.Web.Mvc;
using BloodDonation.Business.Services;
using BloodDonation.Logic.Models;
using BloodDonation.Mappers;
using BloodDonation.Models;
using Firebase.Auth;
using System;
using BloodDonation.Logic.Services;
using BloodDonation.Services;

namespace BloodDonation.Controllers
{
    public class DonorController : Controller
    {
        private readonly BusinessToPresentationMapperDonor
            _businessToPresentationMapper = new BusinessToPresentationMapperDonor();
        
        private readonly PresentationToBusinessMapperDonor _presentationToBusinessMapperDonor = new PresentationToBusinessMapperDonor();
        private readonly DonationCenterServicePresentation donationCenterServicePresentation = new DonationCenterServicePresentation();

        private readonly DonorService donorService = new DonorService();
        private readonly DonationService donationService = new DonationService();

        public ActionResult Index()
        {
            DonorDetailsTransferObject currentDonor = donorService.GetOne(GetUid());

            List<DonationDetails> donationDetails = donationService.FindDonationsByDonorCNP(currentDonor.Cnp);
            
            ShowDonorDonations details = new ShowDonorDonations();
            foreach (DonationDetails detail in donationDetails)
            {
                details.AddDonationDetails(_businessToPresentationMapper.MapDonorDonationDetails(detail));
            }

            return View("ShowDonorDonationsView", details);   
        }

        public ActionResult DonorPersonalDetailsView()
        {
            DonorDetailsTransferObject donorDetailsTransferObject = donorService.GetOne(GetUid());
            DonorAccountRequest donorAccountRequest =
                _businessToPresentationMapper.MapDonorAccountRequest(donorDetailsTransferObject);
            
            return View("DonorPersonalDetailsView", donorAccountRequest);
        }
        
        public ActionResult GetEditDonorPersonalDataPage()
        {
            return View("EditDonorPersonalDataView", new DonorAccountRequest());
        }

        public ActionResult SelectDonationHourView(BookDonationDetails details)
        {
            return View("SelectDonationHourView", details);
        }

        public ActionResult BookDonationView()
        {
            AvailableHoursModel model = new AvailableHoursModel();
            ManageDonationCentersModel manageDonationCentersModel = donationCenterServicePresentation.GetAllDonationCenters();
            model.donationsCenterList = manageDonationCentersModel.GetDonationsCenterName();
            return View("BookDonationView", model);
        }
        
        [HttpPost]
        public ActionResult UpdateDonorPersonalData(DonorAccountRequest formDetails)
        {
           
            donorService.EditDonorDetails(_presentationToBusinessMapperDonor.MapDonorForm(formDetails));
            return DonorPersonalDetailsView();
        }

        [HttpPost]
        public ActionResult SeeAvailableHours(AvailableHoursModel formDetails)
        {
            BookDonationDetails details = new BookDonationDetails();
            details.availableHours = donationService.showAvailableHours(formDetails.bookingDate);
            details.center = formDetails.donationCenter;
            details.donationDate = formDetails.bookingDate;
            return SelectDonationHourView(details);
        }

        
        public ActionResult BookDonation(String selectedHour, String selectedDate)
        {
            
            return DonorPersonalDetailsView();
           
        }

        public String GetUid()
        {
            try
            {
                return ((FirebaseAuthLink)Session["authlink"]).User.LocalId;
            }
            catch (System.Exception e)
            {
                return "";
            }
        }
    }  
}