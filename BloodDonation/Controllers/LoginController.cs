using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Tree;
using BloodDonation.Models;

namespace BloodDonation.Controllers
{
    public class LoginController : Controller
    {
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
            var firstName = form.FirstName;
            return View("LoginHomePage");
        }
    }
}