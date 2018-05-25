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

        public ActionResult Index()
        {
            

            return View("LoginHomePage");
        }

        public ActionResult ShowSignUpPage()
        {
            return View("SignUpView", new SignUpForm());
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> SignUp(SignUpForm form)
        {

            FirebaseAuthLink firebaseAuthLink = await authProvider.CreateUserWithEmailAndPasswordAsync(form.Email, form.Password);
            form.UID = firebaseAuthLink.User.LocalId;


            NewUserTransferObject newUser = _presentationToBusinessMapper.MapNewUserTransferObject(form);

            switch (form.UserType)
            {
                case (int)UserTypeEnum.Doctor:
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