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
        private readonly HospitalService hospitalService = new HospitalService();
        private readonly DonationCenterService donationCenterService = new DonationCenterService();

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

            List<DoctorTransferObject> doctorTransferObjects = doctorService.GetValidDoctors();
            ManageAccountsModel manageAccountsModel = new ManageAccountsModel();
            foreach(DoctorTransferObject dto in doctorTransferObjects)
            {
                manageAccountsModel.AddDoctorAccount(_businessToPresentationMapper.MapDoctorDisplayData(dto));
            }
            return View("ManageDoctorsView", manageAccountsModel);
        }

        public ActionResult GetPersonnelPage()
        {
            List<DonationCenterPersonnelTransferObject> donationCenterPersonnelTransferObjects = donationCenterPersonnelService.GetValidDonationCenterPersonnel();
            ManageAccountsModel manageAccountsModel = new ManageAccountsModel();
            foreach(DonationCenterPersonnelTransferObject dcpto in donationCenterPersonnelTransferObjects)
            {
                manageAccountsModel.AddDonationCenterPersonnelAccount(_businessToPresentationMapper.MapDonationCenterPersonnelDisplayData(dcpto));
            }
            return View("ManagePersonnelView", manageAccountsModel);
        }

        public ActionResult GetHospitalsPage()
        {

            List<HospitalTransferObject> hospitalTransferObjects = hospitalService.GetAllHospitals();
            ManageHospitalsModel manageHospitalsModel = new ManageHospitalsModel();
            foreach(HospitalTransferObject hto in hospitalTransferObjects)
            {
                manageHospitalsModel.AddHospital(_businessToPresentationMapper.MapHospitalDisplayData(hto));
            }
            return View("ManageHospitalsView", manageHospitalsModel);
        }

        public ActionResult GetDonationCentersPage()
        {
            List<DonationCenterTransferObject> donationCenterTransferObjects = donationCenterService.GetAllDonationCenters();
            ManageDonationCentersModel manageDonationCentersModel = new ManageDonationCentersModel();
            foreach(DonationCenterTransferObject dcto in donationCenterTransferObjects)
            {
                manageDonationCentersModel.AddDonationCenter(_businessToPresentationMapper.MapDonationCenterDisplayData(dcto));
            }
            return View("ManageDonationCentersView", manageDonationCentersModel);
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