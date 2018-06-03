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
using System.Threading;

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

        private ActionResult goIfPossible(ActionResult actionResultSuccess)
        {

            Thread.Sleep(1000);

            if (Session["usertype"] == null)
                return RedirectToAction("Index", "Login");
            if ((string)Session["usertype"] != "admin")
                return RedirectToAction("Error", "Error");
            if(Session["authlink"] != null && ((FirebaseAuthLink)Session["authlink"]).IsExpired())
                return RedirectToAction("Index", "Login");

            return actionResultSuccess;
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
            return goIfPossible(View("ManagePersonnelRequestsView", manageRequestsModel));
        }

        public ActionResult GetDoctorAccountRequestsPage()
        {
            List<AccountRequest> doctorAccountRequests = doctorService.GetDoctorAccountRequests();
            ManageRequestsModel manageRequestsModel = new ManageRequestsModel();
            foreach (AccountRequest ar in doctorAccountRequests)
            {
                manageRequestsModel.AddDoctorAccountRequest(_businessToPresentationMapper.MapDoctorAccountRequest(ar));
            }
            return goIfPossible(View("ManageDoctorRequestsView", manageRequestsModel));
        }

        public ActionResult GetDoctorsPage()
        {

            List<DoctorTransferObject> doctorTransferObjects = doctorService.GetValidDoctors();
            ManageAccountsModel manageAccountsModel = new ManageAccountsModel();
            foreach(DoctorTransferObject dto in doctorTransferObjects)
            {
                manageAccountsModel.AddDoctorAccount(_businessToPresentationMapper.MapDoctorDisplayData(dto));
            }
            return goIfPossible(View("ManageDoctorsView", manageAccountsModel));
        }

        public ActionResult GetPersonnelPage()
        {
            List<DonationCenterPersonnelTransferObject> donationCenterPersonnelTransferObjects = donationCenterPersonnelService.GetValidDonationCenterPersonnel();
            ManageAccountsModel manageAccountsModel = new ManageAccountsModel();
            foreach(DonationCenterPersonnelTransferObject dcpto in donationCenterPersonnelTransferObjects)
            {
                manageAccountsModel.AddDonationCenterPersonnelAccount(_businessToPresentationMapper.MapDonationCenterPersonnelDisplayData(dcpto));
            }
            return goIfPossible(View("ManagePersonnelView", manageAccountsModel));
        }

        public ActionResult GetHospitalsPage()
        {

            List<HospitalTransferObject> hospitalTransferObjects = hospitalService.GetAllHospitals();
            ManageHospitalsModel manageHospitalsModel = new ManageHospitalsModel();
            foreach(HospitalTransferObject hto in hospitalTransferObjects)
            {
                manageHospitalsModel.AddHospital(_businessToPresentationMapper.MapHospitalDisplayData(hto));
            }
            return goIfPossible(View("ManageHospitalsView", manageHospitalsModel));
        }

        public ActionResult GetDonationCentersPage()
        {
            List<DonationCenterTransferObject> donationCenterTransferObjects = donationCenterService.GetAllDonationCenters();
            ManageDonationCentersModel manageDonationCentersModel = new ManageDonationCentersModel();
            foreach(DonationCenterTransferObject dcto in donationCenterTransferObjects)
            {
                manageDonationCentersModel.AddDonationCenter(_businessToPresentationMapper.MapDonationCenterDisplayData(dcto));
            }
            return goIfPossible(View("ManageDonationCentersView", manageDonationCentersModel));
        }

        public ActionResult GetCreateAdminPage()
        {
            return goIfPossible(View("CreateAdminView", new CreateAdminForm()));
        }

        public ActionResult GetPersonalDataPage()
        {
            return goIfPossible(View("PersonalDataView"));
        }

        public ActionResult GetEditPersonalDataPage()
        {
            return goIfPossible(View("EditPersonalDataView", new ChangeAdminPersonalDataForm()));
        }

        public ActionResult GetAdminCreatedPage()
        {
            return goIfPossible(View("AdminCreatedView"));
        }

        [HttpPost]
        public ActionResult UpdatePersonalData(ChangeAdminPersonalDataForm form)
        {
            // TODO - somehow update personal data
            return goIfPossible(GetPersonalDataPage());
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
     
            return goIfPossible(View("ManageDoctorsView", model));
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
  
            return goIfPossible(View("ManagePersonnelView", model));

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
            return goIfPossible(GetHospitalsPage());
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
            return goIfPossible(GetDonationCentersPage());
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
            return goIfPossible(View("ManageHospitalsView", model));
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
            return goIfPossible(View("ManageDonationCentersView", model));
        }

        public ActionResult ApproveAccountRequest(string id, string requestType)
        {

            switch (requestType)
            {
                case "Doctor":
                    {
                        doctorService.ApproveAccount(id);
                        return goIfPossible(GetDoctorAccountRequestsPage());
                    }
                case "DonationCenterPersonnel":
                    {
                        donationCenterPersonnelService.ApproveAccount(id);
                        return goIfPossible(GetPersonnelAccountRequestsPage());
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
                        return goIfPossible(GetDoctorAccountRequestsPage());
                    }
                case "DonationCenterPersonnel":
                    {
                        donationCenterPersonnelService.DeleteAccount(id);
                        return goIfPossible(GetPersonnelAccountRequestsPage());
                    }
                default:
                    {
                        return RedirectToAction("Error", "Error");
                    }
            }
        }

        public ActionResult DeleteHospital(string id)
        {
            hospitalService.RemoveById(id);
            return goIfPossible(GetHospitalsPage());
        }

        public ActionResult DeleteDonationCenter(string id)
        {
            donationCenterService.RemoveById(id);
            return goIfPossible(GetDonationCentersPage());
        }

       public ActionResult DeleteDoctor(string id)
        {
            doctorService.DeleteAccount(id);
            return goIfPossible(GetDoctorsPage());
        }

        public ActionResult DeletePersonnel(string id)
        {
            donationCenterPersonnelService.DeleteAccount(id);
            return goIfPossible(GetPersonnelPage());
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