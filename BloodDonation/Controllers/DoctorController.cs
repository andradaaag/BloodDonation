using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {
            return View("DoctorHomeView");
        }

        public ActionResult DoctorHome()
        {
            return View("DoctorHomeView");
        }

        public ActionResult RequestBlood()
        {
            return View("RequestBloodView");
        }

        public ActionResult MyRequests()
        {
            return View("MyRequestsView");
        }

        public ActionResult PersonalDetails()
        {
            return View("DoctorPersonalDetailsView");
        }

        public ActionResult AccountSettings()
        {
            return View("DoctorAccountSettingsView");
        }
    }
}