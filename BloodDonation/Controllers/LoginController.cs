using System.Web.Mvc;
using BloodDonation.Business.Services;
using BloodDonation.Logic.Models;
using BloodDonation.Logic.Services;
using BloodDonation.Mappers;
using BloodDonation.Models;
using BloodDonation.Utils.Enums;
using Firebase.Auth;

namespace BloodDonation.Controllers
{
    public class LoginController : Controller
    {
        private readonly PresentationToBusinessMapper _presentationToBusinessMapper = new PresentationToBusinessMapper();
        private readonly DonorService _donorService = new DonorService();
        private readonly DoctorService _doctorService = new DoctorService();
        private readonly DonationCenterPersonnelService _donationCenterPersonnelService = new DonationCenterPersonnelService();

        static FirebaseConfig config = new FirebaseConfig("AIzaSyBX9u-1P99X08XHfL-rr3DxqJMCVnI4Vbw");
        FirebaseAuthProvider authProvider = new FirebaseAuthProvider(config);

        public async System.Threading.Tasks.Task<ActionResult> Index()
        {

            if (Session["user"] != null && Session["pass"]!=null)
            {
                FirebaseAuthLink firebaseAuthLink = await authProvider.SignInWithEmailAndPasswordAsync((string)Session["user"], (string)Session["pass"]);

                return redirectUser(firebaseAuthLink.User.LocalId);
            }

            return View("LoginHomePage");
            
        }

        public ActionResult ShowSignUpPage()
        {
            return View("SignUpView", new SignUpForm());
        }

        public ActionResult redirectUser(string id)
        {
            if (_doctorService.IsIDPresent(id))
            {
                Session["usertype"] = "doctor";
                Session["localId"] = id; 
                return RedirectToAction("Index", "Doctor");
                
            }
            else if (_donorService.IsIDPresent(id))
            {
                Session["usertype"] = "donor";
                Session["localId"] = id;
                return RedirectToAction("Index", "Donor");
            }
            else if (_donationCenterPersonnelService.IsIDPresent(id))
            {
                Session["usertype"] = "personnel";
                Session["localId"] = id;
                return RedirectToAction("Index", "Personnel");
            }
            else
            {
                Session["usertype"] = "admin";
                Session["localId"] = id;
                return RedirectToAction("Index", "Admin");
            }
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> LogIn(LogInForm logInForm)
        {
            try
            {
                FirebaseAuthLink firebaseAuthLink = await authProvider.SignInWithEmailAndPasswordAsync(logInForm.Username, logInForm.Password);

                Session.Timeout = 480; //in minutes;
                Session["user"] = logInForm.Username;
                Session["pass"] = logInForm.Password;

                Session["authlink"] = firebaseAuthLink;

                return redirectUser(firebaseAuthLink.User.LocalId);
            } 
            catch (Firebase.Auth.FirebaseAuthException e)
            {
                // TODO - implement invalid username and password
                return RedirectToAction("Error", "Error");
            }
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> SignUp(SignUpForm form)
        {

            FirebaseAuthLink firebaseAuthLink = await authProvider.CreateUserWithEmailAndPasswordAsync(form.Email, form.Password);
            form.UID = firebaseAuthLink.User.LocalId;


            NewUserTransferObject newUser = _presentationToBusinessMapper.MapNewUserTransferObject(form);

            switch (form.UserType)
            {
                case (int)Utils.Enums.UserTypeEnum.Doctor:
                    {
                        _doctorService.AddDoctorAccount(newUser);
                        break;
                    }

                case (int)UserTypeEnum.Donor:
                    {
                        _donorService.AddDonorAccount(newUser);
                        break;
                    }

                case (int)UserTypeEnum.Personnel:
                    {
                        _donationCenterPersonnelService.AddDonationCenterPersonnelAccount(newUser);
                        break;
                    }  
                default:
                    {
                        break;
                    }
            }
            return View("LoginHomePage");
            
        }
    }
}