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
using Firebase.Auth;

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

        static FirebaseConfig config = new FirebaseConfig("AIzaSyBX9u-1P99X08XHfL-rr3DxqJMCVnI4Vbw");
        FirebaseAuthProvider authProvider = new FirebaseAuthProvider(config);

        private bool checkUserType()
        {

            //TODO - if unavailable, redirect to login

            if ((string)Session["usertype"] != "admin")
                return false;
            return true;
        }

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
            if(checkUserType())
                return View("ManagePersonnelRequestsView", manageRequestsModel);
            return RedirectToAction("Error", "Error");
        }

        public ActionResult GetDoctorAccountRequestsPage()
        {
            List<AccountRequest> doctorAccountRequests = doctorService.GetDoctorAccountRequests();
            ManageRequestsModel manageRequestsModel = new ManageRequestsModel();
            foreach (AccountRequest ar in doctorAccountRequests)
            {
                manageRequestsModel.AddDoctorAccountRequest(_businessToPresentationMapper.MapDoctorAccountRequest(ar));
            }
            if (checkUserType())
                return View("ManageDoctorRequestsView", manageRequestsModel);
            return RedirectToAction("Error", "Error");
        }

        public ActionResult GetDoctorsPage()
        {

            List<DoctorTransferObject> doctorTransferObjects = doctorService.GetValidDoctors();
            ManageAccountsModel manageAccountsModel = new ManageAccountsModel();
            foreach(DoctorTransferObject dto in doctorTransferObjects)
            {
                manageAccountsModel.AddDoctorAccount(_businessToPresentationMapper.MapDoctorDisplayData(dto));
            }
            if (checkUserType())
                return View("ManageDoctorsView", manageAccountsModel);
            return RedirectToAction("Error", "Error");
        }

        public ActionResult GetPersonnelPage()
        {
            List<DonationCenterPersonnelTransferObject> donationCenterPersonnelTransferObjects = donationCenterPersonnelService.GetValidDonationCenterPersonnel();
            ManageAccountsModel manageAccountsModel = new ManageAccountsModel();
            foreach(DonationCenterPersonnelTransferObject dcpto in donationCenterPersonnelTransferObjects)
            {
                manageAccountsModel.AddDonationCenterPersonnelAccount(_businessToPresentationMapper.MapDonationCenterPersonnelDisplayData(dcpto));
            }
            if (checkUserType())
                return View("ManagePersonnelView", manageAccountsModel);
            return RedirectToAction("Error", "Error");
        }

        public ActionResult GetHospitalsPage()
        {

            List<HospitalTransferObject> hospitalTransferObjects = hospitalService.GetAllHospitals();
            ManageHospitalsModel manageHospitalsModel = new ManageHospitalsModel();
            foreach(HospitalTransferObject hto in hospitalTransferObjects)
            {
                manageHospitalsModel.AddHospital(_businessToPresentationMapper.MapHospitalDisplayData(hto));
            }
            if (checkUserType())
                return View("ManageHospitalsView", manageHospitalsModel);
            return RedirectToAction("Error", "Error");
        }

        public ActionResult GetDonationCentersPage()
        {
            List<DonationCenterTransferObject> donationCenterTransferObjects = donationCenterService.GetAllDonationCenters();
            ManageDonationCentersModel manageDonationCentersModel = new ManageDonationCentersModel();
            foreach(DonationCenterTransferObject dcto in donationCenterTransferObjects)
            {
                manageDonationCentersModel.AddDonationCenter(_businessToPresentationMapper.MapDonationCenterDisplayData(dcto));
            }
            if (checkUserType())
                return View("ManageDonationCentersView", manageDonationCentersModel);
            return RedirectToAction("Error", "Error");
        }

        public ActionResult GetCreateAdminPage()
        {
            if (checkUserType())
                return View("CreateAdminView", new CreateAdminForm());
            return RedirectToAction("Error", "Error");
        }

        public ActionResult GetPersonalDataPage()
        {
            if (checkUserType())
                return View("PersonalDataView");
            return RedirectToAction("Error", "Error");
        }

        public ActionResult GetEditPersonalDataPage()
        {
            if (checkUserType())
                return View("EditPersonalDataView", new ChangeAdminPersonalDataForm());
            return RedirectToAction("Error", "Error");
        }

        public ActionResult GetAdminCreatedPage()
        {
            if (checkUserType())
                return View("AdminCreatedView");
            return RedirectToAction("Error", "Error");
        }

        [HttpPost]
        public ActionResult UpdatePersonalData(ChangeAdminPersonalDataForm form)
        {
            // TODO - somehow update personal data
            if (checkUserType())
                return GetPersonalDataPage();
            return RedirectToAction("Error", "Error");
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
            if (checkUserType())
                return View("ManageDoctorsView", model);
            return RedirectToAction("Error", "Error");
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
            if (checkUserType())
                return View("ManagePersonnelView", model);
            return RedirectToAction("Error", "Error");

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
            if (checkUserType())
                return GetHospitalsPage();
            return RedirectToAction("Error", "Error");
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
            if (checkUserType())
                return GetDonationCentersPage();
            return RedirectToAction("Error", "Error");
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
            if (checkUserType())
                return View("ManageHospitalsView", model);
            return RedirectToAction("Error", "Error");
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
            if (checkUserType())
                return View("ManageDonationCentersView", model);
            return RedirectToAction("Error", "Error");
        }

        public ActionResult ApproveAccountRequest(string id, string requestType)
        {

            switch (requestType)
            {
                case "Doctor":
                    {
                        doctorService.ApproveAccount(id);
                        return GetDoctorAccountRequestsPage();
                    }
                case "Personnel":
                    {
                        donationCenterPersonnelService.ApproveAccount(id);
                        return GetPersonnelAccountRequestsPage();
                    }
                default:
                    {
                        return RedirectToAction("Error", "Error");
                    }
            }
        }

        public ActionResult DenyAccountRequest(string id, string requestType)
        {

            switch (requestType)
            {


                // TOOD - remove from auth as well

                case "Doctor":
                    {
                        doctorService.DeleteAccount(id);
                        return GetDoctorAccountRequestsPage();
                    }
                case "Personnel":
                    {
                        donationCenterPersonnelService.DeleteAccount(id);
                        return GetPersonnelAccountRequestsPage();
                    }
                default:
                    {
                        return RedirectToAction("Error", "Error");
                    }
            }
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