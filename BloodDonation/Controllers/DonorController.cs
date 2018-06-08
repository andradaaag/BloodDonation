using System.Collections.Generic;
using System.Web.Mvc;
using BloodDonation.Business.Services;
using BloodDonation.Logic.Models;
using BloodDonation.Mappers;
using BloodDonation.Models;
using Firebase.Auth;
using System;
using System.Threading;
using BloodDonation.Logic.Services;
using BloodDonation.Services;

namespace BloodDonation.Controllers
{
    public class DonorController : Controller
    {
        private readonly BusinessToPresentationMapperDonor
            _businessToPresentationMapper = new BusinessToPresentationMapperDonor();

        private readonly PresentationToBusinessMapperDonor _presentationToBusinessMapperDonor =
            new PresentationToBusinessMapperDonor();

        private readonly DonationCenterServicePresentation donationCenterServicePresentation =
            new DonationCenterServicePresentation();

        private readonly DonorService donorService = new DonorService();
        private readonly DonationService donationService = new DonationService();
        private readonly BookingService bookingService = new BookingService();
        private readonly DonationCenterService donationCenterService = new DonationCenterService();

        private ActionResult goIfPossible(ActionResult actionResultSuccess)
        {
            Thread.Sleep(1000);

            if (Session["usertype"] == null)
                return RedirectToAction("Index", "Login");
            if ((string) Session["usertype"] != "donor")
                return RedirectToAction("Error", "Error");
            if (Session["authlink"] != null && ((FirebaseAuthLink) Session["authlink"]).IsExpired())
                return RedirectToAction("Index", "Login");

            return actionResultSuccess;
        }

        public ActionResult Index()
        {
            DonorDetailsTransferObject currentDonor = donorService.GetOne(GetUid());

            List<Donation> donationDetails = donationService.FindDonationsByDonorCNP(currentDonor.Cnp);
            Donation lastDonation = donationService.DonorLastDonation(currentDonor.Cnp);

            ShowDonorDonations details = new ShowDonorDonations();
            if(lastDonation == null)
            {
                details.LastDonationDate = "No donations";
                details.NextPossibleDonation = "You can donate right now";
            }
            else
            {
                DateTime lastTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToLocalTime().AddSeconds(lastDonation.DonationTime);
                details.LastDonationDate = lastTime.ToString("dd-MM-yyyy");
                details.NextPossibleDonation = lastTime.AddMonths(6).ToString("dd-MM-yyyy");
            }

            foreach (Donation detail in donationDetails)
            {
                DonationCenterTransferObject dcto = donationCenterService.GetDonationCenterById(detail.DonationCenterId);
                if(dcto != null)
                    detail.DonationCenterId = dcto.Name;
                details.AddDonationDetails(_businessToPresentationMapper.MapDonorDonationDetails(detail));
            }

            return goIfPossible(View("ShowDonorDonationsView", details));
        }

        public ActionResult DonorPersonalDetailsView()
        {
            DonorDetailsTransferObject donorDetailsTransferObject = donorService.GetOne(GetUid());
            DonorAccountRequest donorAccountRequest =
                _businessToPresentationMapper.MapDonorAccountRequest(donorDetailsTransferObject);

            return goIfPossible(View("DonorPersonalDetailsView", donorAccountRequest));
        }

        public ActionResult GetEditDonorPersonalDataPage(DonorAccountRequest details)
        {
            return goIfPossible(View("EditDonorPersonalDataView", details));
        }

        public ActionResult SelectDonationHourView(BookDonationDetails details)
        {
            return goIfPossible(View("SelectDonationHourView", details));
        }

        public ActionResult ShowBookedDatesDonorPage()
        {
            ShowBookingDate model = new ShowBookingDate();
            model.donorBookedDates = new List<BookedDates>();
            foreach (BookedHoursTransferObject btho in bookingService.GetDonorBookedHours(GetUid()))
            {
                DonationCenterTransferObject dcto = donationCenterService.GetDonationCenterById(btho.center);
                model.AddDate(_businessToPresentationMapper.MapBookedDates(btho, dcto.Name));
            }

            return goIfPossible(View("ShowDonorBookedDatesView", model));
        }

        public ActionResult BookDonationView()
        {
            AvailableHoursModel model = new AvailableHoursModel();
            ManageDonationCentersModel manageDonationCentersModel =
                donationCenterServicePresentation.GetAllDonationCenters();
            model.donationsCenterList = manageDonationCentersModel.GetDonationsCenterName();
            return goIfPossible(View("BookDonationView", model));
        }

        [HttpPost]
        public ActionResult UpdateDonorPersonalData(DonorAccountRequest formDetails)
        {
            formDetails.ID = GetUid();
            donorService.EditDonorDetails(_presentationToBusinessMapperDonor.MapDonorForm(formDetails));
            return goIfPossible(DonorPersonalDetailsView());
        }

        [HttpPost]
        public ActionResult SeeAvailableHours(AvailableHoursModel formDetails)
        {
            BookDonationDetails details = new BookDonationDetails();
            details.center = formDetails.donationCenter;
            details.donationDate = formDetails.bookingDate;

            DonationCenterTransferObject dcto = donationCenterService.FindByName(details.center);

            details.availableHours = bookingService.GetAvailableHours(dcto.ID, formDetails.bookingDate);
            return goIfPossible(SelectDonationHourView(details));
        }


        public ActionResult BookDonation(String selectedHour, String selectedDate, String center)
        {
            DonationCenterTransferObject dcto = donationCenterService.FindByName(center);


            Booking newBooking = new Booking();

            newBooking.Date = selectedDate;
            newBooking.Hour = selectedHour;
            newBooking.DonorId = GetUid();
            newBooking.DonorName = donorService.GetOne(newBooking.DonorId).LastName + " " +
                                   donorService.GetOne(newBooking.DonorId).FirstName;
            newBooking.DonationCenterId = dcto.ID;

            bookingService.saveBooking(newBooking);

            return goIfPossible(ShowBookedDatesDonorPage());
        }

        public String GetUid()
        {
            try
            {
                return ((FirebaseAuthLink) Session["authlink"]).User.LocalId;
            }
            catch (System.Exception e)
            {
                return "";
            }
        }
    }
}