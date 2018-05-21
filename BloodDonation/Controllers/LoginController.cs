using System.Web.Mvc;
using BloodDonation.Business.Services;
using BloodDonation.Logic.Services;
using BloodDonation.Mappers;
using BloodDonation.Models;

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
    }
}