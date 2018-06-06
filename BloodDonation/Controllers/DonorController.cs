
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
        private readonly BookingService bookingService = new BookingService();
        private readonly DonationCenterService donationCenterService = new DonationCenterService();

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

        public ActionResult ShowBookedDatesDonorPage()
        {
            ShowBookingDate model = new ShowBookingDate();
            model.donorBookedDates= new List<BookedDates>();
            foreach (BookedHoursTransferObject btho in bookingService.GetDonorBookedHours(GetUid())){
                DonationCenterTransferObject dcto = donationCenterService.GetDonationCenterById(btho.center);
                model.AddDate(_businessToPresentationMapper.MapBookedDates(btho, dcto.Name));

            }

            return View("ShowDonorBookedDatesView", model); 
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
            details.center = formDetails.donationCenter;
            details.donationDate = formDetails.bookingDate;

            DonationCenterTransferObject dcto = donationCenterService.FindByName(details.center);

            details.availableHours = bookingService.GetAvailableHours(dcto.ID, formDetails.bookingDate);
            return SelectDonationHourView(details);
        }

        
        public ActionResult BookDonation(String selectedHour, String selectedDate, String center)
        {
            DonationCenterTransferObject dcto = donationCenterService.FindByName(center);


            Booking newBooking = new Booking();

            newBooking.Date = selectedDate;
            newBooking.Hour = selectedHour;
            newBooking.DonorId = GetUid();
            newBooking.DonorName = donorService.GetOne(newBooking.DonorId).LastName + " " + donorService.GetOne(newBooking.DonorId).FirstName;
            newBooking.DonationCenterId = dcto.ID;

            bookingService.saveBooking(newBooking);

            return ShowBookedDatesDonorPage();

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