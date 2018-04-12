using System.Web.Mvc;
using BloodDonation.Logic.Services;
using BloodDonation.Mappers;
using BloodDonation.Models;

namespace BloodDonation.Controllers
{
    public class LoginController : Controller
    {
        private readonly PresentationToLogicMapper presentationToLogicMapper = new PresentationToLogicMapper();
        private readonly DonorService donorService = new DonorService();

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
            var donationForm = presentationToLogicMapper.MapDonationForm(form);
            donorService.AddDonationForm(donationForm);
            return View("LoginHomePage");
        }
    }
}