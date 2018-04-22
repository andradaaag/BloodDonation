using BloodDonation.Logic.Models;
using BloodDonation.Logic.Services;
using BloodDonation.Mappers;
using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class AdminController : Controller
    {

        private readonly PresentationToBusinessMapper _presentationToBusinessMapper = new PresentationToBusinessMapper();
        private readonly BusinessToPresentationMapper _businessToPresentationMapper = new BusinessToPresentationMapper();
        private readonly DoctorService doctorService = new DoctorService();
        private readonly DonationCenterPersonnelService donationCenterPersonnelService = new DonationCenterPersonnelService();

        // GET: Admin
        public ActionResult Index()
        {
            return GetDoctorAccountRequestsPage();
        }

        public ActionResult GetPersonnelAccountRequestsPage()
        {

            List<AccountRequest> donationCenterPersonnelAccountRequests = donationCenterPersonnelService.GetDonationCenterPersonnelAccountRequests();
            ManageRequestsModel manageRequestsModel = new ManageRequestsModel();
            foreach (AccountRequest ar in donationCenterPersonnelAccountRequests)
            {
                manageRequestsModel.AddDonationCenterPersonnelAccountRequest(_businessToPresentationMapper.MapDonationCenterPersonnelAccountRequest(ar));
            }
            return View("ManagePersonnelRequestsView", manageRequestsModel);
        }

        public ActionResult GetDoctorAccountRequestsPage()
        {
            List<AccountRequest> doctorAccountRequests = doctorService.GetDoctorAccountRequests();
            ManageRequestsModel manageRequestsModel = new ManageRequestsModel();
            foreach (AccountRequest ar in doctorAccountRequests)
            {
                manageRequestsModel.AddDoctorAccountRequest(_businessToPresentationMapper.MapDoctorAccountRequest(ar));
            }
            return View("ManageDoctorRequestsView", manageRequestsModel);
        }

        public ActionResult GetDoctorsPage()
        {
            return View("ManageDoctorsView");
        }

        public ActionResult GetPersonnelPage()
        {
            return View("ManagePersonnelView");
        }

        public ActionResult GetHospitalsPage()
        {
            return View("ManageHospitalsView");
        }

        public ActionResult GetDonationCentersPage()
        {
            return View("ManageDonationCentersView");
        }

        public ActionResult GetCreateAdminPage()
        {
            return View("CreateAdminView");
        }

        public ActionResult GetPersonalDataPage()
        {
            return View("PersonalDataView");
        }
    }
}