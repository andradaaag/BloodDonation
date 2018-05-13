using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Security;
using BloodDonation.Business.Services;
using BloodDonation.Mappers;
using BloodDonation.Models;
using BloodDonation.Utils.Firebase;
using BloodDonation.Utils.Models;
using Newtonsoft.Json;

namespace BloodDonation.Controllers
{
    public class LoginController : Controller
    {
        private readonly PresentationToBusinessMapper _presentationToBusinessMapper = new PresentationToBusinessMapper();
        private readonly DonorService _donorService = new DonorService();

        public ActionResult Index()
        {
            return View("LoginHomePage");
        }


        [HttpPost]
        public string Test(string p)
        {
            string uid = new StreamReader(Request.InputStream).ReadToEnd();
            if (uid == "")
                return "";
            FirebaseDatabase firebaseDB = new FirebaseDatabase("https://blooddonation-bc0b9.firebaseio.com/");
            FirebaseDatabase firebaseUsers = firebaseDB.NodePath("users/"+uid);
            FirebaseResponse response = firebaseUsers.Get();
            UserData ud = JsonConvert.DeserializeObject<UserData>(response.JsonContent);

       
            string role = ud.role ?? "undefined";
            System.Diagnostics.Debug.WriteLine("===============");
            System.Diagnostics.Debug.WriteLine(HttpContext.User.Identity);

            //TODO: add proper pages
            //Roles.AddUserToRole(uid, role);
            return role;
            //switch (role)
            //{
            //    case "admin":
            //        return RedirectToAction("Authenticated");
            //    case "donor":
            //        return RedirectToAction("Donor");
            //    case "doctor":
            //        return RedirectToAction("Doctor");
            //    case "personnel":
            //        return RedirectToAction("Personnel");
            //    default:
            //        return RedirectToAction("Index");
            //}
        }


        public ActionResult ShowSignUpPage()
        {
            return View("SignUpView", new SignUpForm());
        }

        [HttpPost]
        public ActionResult SignUp(SignUpForm form)
        {
            var donationForm = _presentationToBusinessMapper.MapDonationForm(form);
            _donorService.AddDonationForm(donationForm);
            return View("LoginHomePage");
        }

        //[Authorize(Roles = UserTypes.Admin)]
        public ActionResult Authenticated()
        {
            return View();
        }

        //[Authorize(Roles = UserTypes.Donor)]
        public ActionResult Donor()
        {
            return View();
        }

        //[Authorize(Roles = UserTypes.Doctor)]
        public ActionResult Doctor()
        {
            return View();
        }

        //[Authorize(Roles = UserTypes.Personnel)]
        public ActionResult Personnel()
        {
            return View();
        }
    }
}