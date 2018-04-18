using BloodDonation.Logic.Models;
using BloodDonation.Logic.Services;
using BloodDonation.Mappers;
using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class AdminController : Controller
    {

        private readonly PresentationToBusinessMapper _presentationToBusinessMapper = new PresentationToBusinessMapper();
        private readonly BusinessToPresentationMapper _businessToPresentationMapper = new BusinessToPresentationMapper();
        private readonly DoctorService doctorService = new DoctorService();

        // GET: Admin
        public ActionResult Index()
        {
            List<AccountRequest> doctorAccountRequests = doctorService.GetDoctorAccountRequests();
            ManageRequestsModel manageRequestsModel = new ManageRequestsModel();
            foreach(AccountRequest ar in doctorAccountRequests)
            {
                manageRequestsModel.AddDoctorAccountRequest(_businessToPresentationMapper.MapDoctorAccountRequest(ar));
            }
            return View("ManageRequestsView", manageRequestsModel);
        }
    }
}