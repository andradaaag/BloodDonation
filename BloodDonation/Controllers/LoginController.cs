using System;
using System.Collections.Generic;
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
        private readonly PresentationToBusinessMapper
            _presentationToBusinessMapper = new PresentationToBusinessMapper();

        private readonly DonorService _donorService = new DonorService();
        private readonly DoctorService _doctorService = new DoctorService();
        private readonly HospitalService _hospitalService = new HospitalService();
        private readonly DonationCenterService _donationCenterService = new DonationCenterService();

        private readonly DonationCenterPersonnelService _donationCenterPersonnelService = new DonationCenterPersonnelService();

        static FirebaseConfig config = new FirebaseConfig("AIzaSyBX9u-1P99X08XHfL-rr3DxqJMCVnI4Vbw");
        FirebaseAuthProvider authProvider = new FirebaseAuthProvider(config);

        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            if (Session["user"] != null && Session["pass"] != null)
            {
                FirebaseAuthLink firebaseAuthLink =
                    await authProvider.SignInWithEmailAndPasswordAsync((string) Session["user"],
                        (string) Session["pass"]);

                return redirectUser(firebaseAuthLink.User.LocalId);
            }

            return View("LoginHomePage");
        }

        private List<SelectListItem> GetHospitalList()
        {
            var result = new List<SelectListItem>();
            var hospitals = _hospitalService.GetAllHospitals();

            result.Add(new SelectListItem
            {
                Text = "Choose a hospital",
                Value = "",
                Selected = true
            });

            foreach (var hospital in hospitals)
            {
                result.Add(new SelectListItem
                {
                    Text = hospital.Name,
                    Value = hospital.ID
                });
            }

            result[0].Selected = true;

            return result;
        }

        private List<SelectListItem> GetDonationCenterList()
        {
            var result = new List<SelectListItem>();
            var centers = _donationCenterService.GetAllDonationCenters();

            result.Add(new SelectListItem
            {
                Text = "Choose a center",
                Value = "",
                Selected = true
            });

            foreach (var center in centers)
            {
                result.Add(new SelectListItem
                {
                    Text = center.Name,
                    Value = center.ID
                });
            }

            return result;
        }

        public ActionResult ShowSignUpPage()
        {
            ViewData["hospitals"] = GetHospitalList();
            ViewData["centers"] = GetDonationCenterList();
            return View("SignUpView", new SignUpForm());
        }


        // TODO - this is really ugly code, something can definitely be done here
        public ActionResult redirectUser(string id)
        {
            if (_doctorService.exists(id))
            {
                if( _doctorService.isValidAccount(id))
                {
                    Session["usertype"] = "doctor";
                    return RedirectToAction("Index", "Doctor");
                } else
                {
                    return RedirectToAction("Error", "Error"); // TODO - implement account is invalid
                }
            }
            else if (_donorService.exists(id))
            {
                Session["usertype"] = "donor";
                return RedirectToAction("Index", "Donor");
            }
            else if (_donationCenterPersonnelService.exists(id))
            {
                if (_donationCenterPersonnelService.isValidAccount(id))
                {
                    Session["usertype"] = "personnel";
                    return RedirectToAction("Index", "Personnel");
                }
                else
                {
                    return RedirectToAction("Error", "Error"); // TODO - implement account is invalid
                }

            }
            else
            {
                Session["usertype"] = "admin";
                return RedirectToAction("Index", "Admin");
            }
        }




        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> LogIn(LogInForm logInForm)
        {
            try
            {
                FirebaseAuthLink firebaseAuthLink =
                    await authProvider.SignInWithEmailAndPasswordAsync(logInForm.Username, logInForm.Password);

                Session.Timeout = 480; //in minutes;
                Session["user"] = logInForm.Username;
                Session["pass"] = logInForm.Password;

                Session["authlink"] = firebaseAuthLink;

                return redirectUser(firebaseAuthLink.User.LocalId);
            }
            catch (Firebase.Auth.FirebaseAuthException)
            {
                // TODO - implement invalid username and password
                return RedirectToAction("Error", "Error");
            }
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> SignUp(SignUpForm form)
        {
            FirebaseAuthLink firebaseAuthLink =
                await authProvider.CreateUserWithEmailAndPasswordAsync(form.Email, form.Password);
            form.UID = firebaseAuthLink.User.LocalId;


            NewUserTransferObject newUser = _presentationToBusinessMapper.MapNewUserTransferObject(form);
            Console.Write(newUser.DonationCenter);

            switch (form.UserType)
            {
                case (int) Utils.Enums.UserTypeEnum.Doctor:
                {
                    _doctorService.AddDoctorAccount(newUser);
                    break;
                }

                case (int) UserTypeEnum.Donor:
                {
                    _donorService.AddDonorAccount(newUser);
                    break;
                }

                case (int) UserTypeEnum.Personnel:
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

        public ActionResult LogOut()
        {
            Session["user"] = null;
            Session["pass"] = null;
            Session["usertype"] = null;
            Session["authlink"] = null;

            return RedirectToAction("Index", "Login");
        }
    }
}