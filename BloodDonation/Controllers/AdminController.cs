using BloodDonation.Models;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web.Mvc;
using Firebase.Auth;
using System.Threading;
using BloodDonation.Services;
using System;

namespace BloodDonation.Controllers
{
    public class AdminController : Controller
    {

        private readonly DonationCenterPersonnelServicePresentation donationCenterPersonnelServicePresentation = new DonationCenterPersonnelServicePresentation();
        private readonly DoctorServicePresentation doctorServicePresentation = new DoctorServicePresentation();
        private readonly HospitalServicePresentation hospitalServicePresentation = new HospitalServicePresentation();
        private readonly DonationCenterServicePresentation donationCenterServicePresentation = new DonationCenterServicePresentation();

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

        public ActionResult Index()
        {
            return GetDoctorAccountRequestsPage();
        }

        public ActionResult GetPersonnelAccountRequestsPage()
        {
            ManageRequestsModel personnelRequestsModel = donationCenterPersonnelServicePresentation.GetDonationCenterPersonnelAccountRequests();
            return goIfPossible(View("ManagePersonnelRequestsView", personnelRequestsModel));
        }

        public ActionResult GetDoctorAccountRequestsPage()
        {
            ManageRequestsModel doctorRequestsModel = doctorServicePresentation.GetDoctorAccountRequests();
            return goIfPossible(View("ManageDoctorRequestsView", doctorRequestsModel));
        }

        public ActionResult GetDoctorsPage()
        {

            ManageAccountsModel doctorAccountsModel = doctorServicePresentation.GetDoctorAccounts();
            return goIfPossible(View("ManageDoctorsView", doctorAccountsModel));
        }

        public ActionResult GetPersonnelPage()
        {
            ManageAccountsModel personnelAccountsModel = donationCenterPersonnelServicePresentation.GetDonationCenterPersonnelAccounts();
            return goIfPossible(View("ManagePersonnelView", personnelAccountsModel));
        }

        public ActionResult GetHospitalsPage()
        {
            ManageHospitalsModel manageHospitalsModel = hospitalServicePresentation.GetAllHospitals();
            return goIfPossible(View("ManageHospitalsView", manageHospitalsModel));
        }

        public ActionResult GetDonationCentersPage()
        {
            ManageDonationCentersModel manageDonationCentersModel = donationCenterServicePresentation.GetAllDonationCenters(); 
            return goIfPossible(View("ManageDonationCentersView", manageDonationCentersModel));
        }

        [HttpPost]
        public ActionResult SearchDoctors(ManageAccountsModel model)
        {
            return goIfPossible(View("ManageDoctorsView", doctorServicePresentation.SearchDoctors(model)));
        }

        [HttpPost]
        public ActionResult SearchDonationCenterPersonnel(ManageAccountsModel model)
        {
            return goIfPossible(View("ManagePersonnelView", donationCenterPersonnelServicePresentation.SearchDonationCenterPersonnel(model)));
        }

        [HttpPost]
        public ActionResult AddNewHospital(ManageHospitalsModel model)
        {
            hospitalServicePresentation.AddNewHospital(model);
            return goIfPossible(GetHospitalsPage());
        }

        [HttpPost]
        public ActionResult AddNewDonationCenter(ManageDonationCentersModel model)
        {
            donationCenterServicePresentation.AddNewDonationCenter(model);
            return goIfPossible(GetDonationCentersPage());
        }

        [HttpPost]
        public ActionResult SearchHospitals(ManageHospitalsModel model)
        {
            return goIfPossible(View("ManageHospitalsView", hospitalServicePresentation.SearchHosptials(model)));
        }

        [HttpPost]
        public ActionResult SearchDonationCenters(ManageDonationCentersModel model)
        {
            return goIfPossible(View("ManageDonationCentersView", donationCenterServicePresentation.SearchDonationCenters(model)));
        }

        public ActionResult ApproveAccountRequest(string id, string requestType)
        {

            switch (requestType)
            {
                case "Doctor":
                    {
                        doctorServicePresentation.ApproveAccount(id);
                        return goIfPossible(GetDoctorAccountRequestsPage());
                    }
                case "DonationCenterPersonnel":
                    {
                        donationCenterPersonnelServicePresentation.ApproveAccount(id);
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
                        doctorServicePresentation.DeleteAccount(id);
                        return goIfPossible(GetDoctorAccountRequestsPage());
                    }
                case "DonationCenterPersonnel":
                    {
                        donationCenterPersonnelServicePresentation.DeleteAccount(id);
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
            hospitalServicePresentation.RemoveById(id);
            return goIfPossible(GetHospitalsPage());
        }

        public ActionResult DeleteDonationCenter(string id)
        {
            donationCenterServicePresentation.RemoveById(id);
            return goIfPossible(GetDonationCentersPage());
        }

       public ActionResult DeleteDoctor(string id)
        {
            doctorServicePresentation.DeleteAccount(id);
            return goIfPossible(GetDoctorsPage());
        }

        public ActionResult DeletePersonnel(string id)
        {
            donationCenterPersonnelServicePresentation.DeleteAccount(id);
            return goIfPossible(GetPersonnelPage());
        }
    }
}