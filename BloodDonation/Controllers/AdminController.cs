using BloodDonation.Logic.Models;
using BloodDonation.Logic.Services;
using BloodDonation.Mappers;
using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

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
            return View("CreateAdminView", new CreateAdminForm());
        }

        public ActionResult GetPersonalDataPage()
        {
            return View("PersonalDataView");
        }

        public ActionResult GetEditPersonalDataPage()
        {
            return View("EditPersonalDataView", new ChangeAdminPersonalDataForm());
        }

        public ActionResult GetAdminCreatedPage()
        {
            return View("AdminCreatedView");
        }

        [HttpPost]
        public ActionResult UpdatePersonalData(ChangeAdminPersonalDataForm form)
        {
            // TODO - somehow update personal data
            return GetPersonalDataPage();
        }

        [HttpPost]
        public ActionResult SearchDoctors(ManageAccountsModel model)
        {
            string searchQuery = model.SearchQuery;

            List<DoctorTransferObject> doctorTransferObjects = doctorService.FilterDoctorsBySearchQuery(searchQuery.Replace(" ", ""));

            model.ResetDoctorAccounts();
            model.IsViewingSearchResults = true;

            foreach (DoctorTransferObject dto in doctorTransferObjects)
            {
                model.AddDoctorAccount(_businessToPresentationMapper.MapDoctorDisplayData(dto));
            }

            return View("ManageDoctorsView", model);
        }

        [HttpPost]
        public ActionResult SearchDonationCenterPersonnel(ManageAccountsModel model)
        {

            string searchQuery = model.SearchQuery;

            List<DonationCenterPersonnelTransferObject> donationCenterPersonnelTransferObjects = donationCenterPersonnelService.FilterDonationCenterPersonnelBySearchQuery(searchQuery.Replace(" ", ""));

            model.ResetDonationCenterPersonnelAccounts();
            model.IsViewingSearchResults = true;

            foreach(DonationCenterPersonnelTransferObject dcpto in donationCenterPersonnelTransferObjects)
            {
                model.AddDonationCenterPersonnelAccount(_businessToPresentationMapper.MapDonationCenterPersonnelDisplayData(dcpto));
            }

            return View("ManagePersonnelView", model);

        }

        [HttpPost]
        public ActionResult AddNewHospital(ManageHospitalsModel model)
        {
            HospitalTransferObject newHospital = new HospitalTransferObject
            {
                Location = model.Location,
                Name = model.Name
            };

            hospitalService.AddNewHospital(newHospital);

            return GetHospitalsPage();
        }

        [HttpPost]
        public ActionResult AddNewDonationCenter(ManageDonationCentersModel model)
        {
            DonationCenterTransferObject newDC = new DonationCenterTransferObject
            {
                Location = model.Location,
                Name = model.Name
            };

            donationCenterService.AddNewDonationCenter(newDC);

            return GetDonationCentersPage();
        }

        [HttpPost]
        public ActionResult SearchHospitals(ManageHospitalsModel model)
        {
            string nameSearchQuery = model.SearchName;
            string locationSearchQuery = model.SearchLocation;

            List<HospitalTransferObject> hospitalTransferObjects = hospitalService.FilterHospitalsByNameAndLocation(nameSearchQuery, locationSearchQuery);

            model.ResetHospitals();
            model.IsViewingSearchResults = true;

            foreach(HospitalTransferObject hto in hospitalTransferObjects)
            {
                model.AddHospital(_businessToPresentationMapper.MapHospitalDisplayData(hto));
            }

            return View("ManageHospitalsView", model);
        }

        [HttpPost]
        public ActionResult SearchDonationCenters(ManageDonationCentersModel model)
        {
            string nameSearchQuery = model.SearchName;
            string locationSearchQuery = model.SearchLocation;

            List<DonationCenterTransferObject> donationCenterTransferObjects = donationCenterService.FilterDonationCentersByNameAndLocation(nameSearchQuery, locationSearchQuery);

            model.ResetDonationCenters();
            model.IsViewingSearchResults = true;

            foreach(DonationCenterTransferObject dcto in donationCenterTransferObjects)
            {
                model.AddDonationCenter(_businessToPresentationMapper.MapDonationCenterDisplayData(dcto));
            }

            return View("ManageDonationCentersView", model);
        }

        [HttpPost]
        public ActionResult CreateAdmin(CreateAdminForm form)
        {
            MailMessage message = new System.Net.Mail.MailMessage();
            message.From = new MailAddress("blooddonationiss@gmail.com", "Blood Donation App");
            message.To.Add(new MailAddress(form.EmailAddress));
            message.Subject = "this is a test email.";
            message.Priority = MailPriority.High;
            message.IsBodyHtml = false;
            message.Sender = new MailAddress("blooddonationiss@gmail.com", "Sender Name");
            message.Body = "this is my test email body for " + form.FirstName + " " + form.LastName;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new System.Net.NetworkCredential("blooddonationiss@gmail.com", "bdisspass8");

            smtp.Send(message);

            return GetAdminCreatedPage();
        }
    }
}