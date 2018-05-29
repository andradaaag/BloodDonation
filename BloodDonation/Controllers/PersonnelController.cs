using BloodDonation.Logic   .Services;
using BloodDonation.Mappers;
using BloodDonation.Models;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class PersonnelController : Controller
    {
        private DonationService donationService = new DonationService();
        private PersonnelService personnelService = new PersonnelService();
        private RequestService requestService = new RequestService();
        private StoredBloodService storedBloodService = new StoredBloodService();
        private DoctorService doctorService = new DoctorService();
        private UserService userService = new UserService();

        private ErrorController errorController = new ErrorController();

        private BusinessToPresentationMapperPersonnel BusinessToPresentation = new BusinessToPresentationMapperPersonnel();
        private PresentationToBusinessMapperPersonnel PresentationToBusiness = new PresentationToBusinessMapperPersonnel();

        public ActionResult Index()
        {
            if (IsNotPersonnel())
                return errorController.Error();
            return View("AddDonationView");
        }

        public ActionResult Success()
        {
            return View("SuccessView");
        }

        public ActionResult AddDonation()
        {
            if (IsNotPersonnel())
                return errorController.Error();
            return View("AddDonationView");
        }

        public ActionResult Home()
        {
            return Index();
        }

        public ActionResult PersonalDetails()
        {
            return View("PersonalDetailsView", BusinessToPresentation.Personnel( personnelService.GetOne(GetUid())));
        }

        [HttpPost]
        public ActionResult AddDonationInDb(DonationModel donation)
        {
            if (IsNotPersonnel())
                return errorController.Error();
            personnelService.AddDonationInDB(PresentationToBusiness.Donation(donation), GetUid(),donation.KeepWhole);
            return Success();
        }

        //START SEPARATE COMPONENTS
        public ActionResult SeparateComponents()
        {
            if (IsNotPersonnel())
                return errorController.Error();

            DonationSepModel dlm = new DonationSepModel
            {
                Donations = donationService
                    .FindByDonCenterForCompSep(GetUid())
                    .Select(i=>BusinessToPresentation.Donation(i))
                    .ToList()
                ,
                StoredBlood = storedBloodService
                    .GetWholeStoredBloodByDonCent(GetUid())
                    .Select(i => BusinessToPresentation.StoredBlood(i))
                    .ToList()
            };
            return View("SeparateComponentsView", dlm);
        }

        [HttpPost]
        public ActionResult EditDonationSeparation(DonationModel donation)
        {
            if (IsNotPersonnel())
                return errorController.Error();
            return View("EditDonationSeparation", donation);
        }

        [HttpPost]
        public ActionResult EditBloodSeparation(StoredBloodModel stored)
        {
            if (IsNotPersonnel())
                return errorController.Error();
            SeparateStoredBloodModel blood = new SeparateStoredBloodModel
            {
                ID = stored.ID,
                DonationCenterID = stored.DonationCenterID,
                BloodTypeGroup = stored.BloodTypeGroup,
                BloodTypeRH = stored.BloodTypeRH,
                CollectionDate = (stored.CollectionDate - new DateTime(1970, 1, 1)).Seconds
            };
            return View("EditBloodSeparation", blood);
        }

        [HttpPost]
        public ActionResult SeparateComponentsToDB(DonationModel donation)
        {
            if (IsNotPersonnel())
                return errorController.Error();
            personnelService.SeparateComponentsFromDonation(PresentationToBusiness.Donation(donation));
            //TODO: maybe remove sleep
            Thread.Sleep(1000);
            return SeparateComponents();
        }

        [HttpPost]
        public ActionResult SeparateBloodComponentsToDB(SeparateStoredBloodModel storedBlood)
        {
            if (IsNotPersonnel())
                return errorController.Error();
            personnelService.SeparateComponentsFromBlood(PresentationToBusiness.SeparateBlood(storedBlood));
            //TODO: maybe remove sleep
            Thread.Sleep(1000);
            return SeparateComponents();
        }
        //END SEPARATE COMPONENTS

       

        //START LAB RESULTS
        public ActionResult LabResults()
        {
            if (IsNotPersonnel())
                return errorController.Error();
            DonationListModel dlm = new DonationListModel
            {
                Donations = donationService
                .FindByDonCenterForLabRes(GetUid())
                .AsEnumerable()
                .Select(i=>BusinessToPresentation.Donation(i))
                .ToList()
            };
            return View("LabResultsView", dlm);
        }
       
        [HttpPost]
        public ActionResult EditDonationLab(DonationModel donation)
        {
            if (IsNotPersonnel())
                return errorController.Error();
            return View("EditDonationLabView", donation);
        }

        [HttpPost]
        public ActionResult LabResultsToDB(DonationModel donation)
        {
            if (IsNotPersonnel())
                return errorController.Error();
            personnelService.CommitLabResults(PresentationToBusiness.Donation(donation));

            Thread.Sleep(1000);
            return LabResults();
        }
        //END LAB RESULTS

        public ActionResult AcceptRequest(string id)
        {
            RequestPersonnel r = BusinessToPresentation.Request(requestService.GetOne(id));
            Personnel loggedPersonnel = BusinessToPresentation.Personnel(personnelService.GetOne(GetUid()));
            string donationCenterID = loggedPersonnel.DonationCenterID;
            int missingBlood = requestService.GetMissingBlood(donationCenterID, PresentationToBusiness.Request(r));

            if (missingBlood > 0)
            {
                String param = Convert.ToString(missingBlood);
                return View("MissingBloodView", model: param);
            }

            r = AddDoctorEmail(r);
            r.quantityString = Convert.ToString(r.quantity);
            return View("AcceptRequestView", r);
        }

        public ActionResult ConfirmAcceptRequest(string id)
        {
            requestService.EditStatus(id, PresentationToBusiness.Status(Status.Accepted));
            Personnel loggedPersonnel = BusinessToPresentation.Personnel(personnelService.GetOne(GetUid()));
            string donationCenterID = loggedPersonnel.DonationCenterID;
            requestService.EditSource(id, donationCenterID);

            RequestPersonnel r = BusinessToPresentation.Request(requestService.GetOne(id));
            storedBloodService.RemoveBlood(donationCenterID, r.quantity,
                PresentationToBusiness.BloodType(r.bloodType));

            return Success();
        }

        public ActionResult EditRequest(string id)
        {
            RequestPersonnel r = BusinessToPresentation.Request(requestService.GetOne(id));
            NewStatus ns = new NewStatus();
            ns.ID = r.ID;
            ns.status = r.status.ToString();
            return View("EditRequestView", ns);
        }


        [HttpPost]
        public ActionResult RequestToDb(NewStatus ns)
        {
            Status s = (Status)Enum.Parse(typeof(Status), ns.status);
            RequestPersonnel previousRequest = BusinessToPresentation.Request(requestService.GetOne(ns.ID));

            previousRequest.status = s;
            requestService.Edit(PresentationToBusiness.Request(previousRequest));
            return Success();

        }


        public ActionResult AcceptedRequests()
        {
            RequestList rl = new RequestList
            {
                Requests = GetDonationCenterRequests()
                    .AsEnumerable()
                    .ToList()
            };
            return View("PendingRequestsView", rl);
        }

        public ActionResult PendingRequests()
        {
            RequestList rl = new RequestList
            {
                Requests = GetUnsolvedRequests()
                .AsEnumerable()
                .ToList()
            };
            return View("PendingRequestsView", rl);
        }

        public ActionResult Requests()
        {
            return View("RequestsView");
        }

        [HttpPost]
        public ActionResult UpdatePersonnel(Personnel p)
        {
            personnelService.Edit(PresentationToBusiness.Personnel(p));
            return Success();
        }


        //UTIL
        public List<RequestPersonnel> GetAllRequests()
        {
            return requestService
                .FindAll()
                .AsEnumerable()
                .Select(i => BusinessToPresentation.Request(i))
                .Select(x => AddDoctorEmail(x))
                .ToList();
        }

        public List<RequestPersonnel> GetUnsolvedRequests()
        {
            return requestService
                .FindUnsolved()
                .AsEnumerable()
                .Select(i => BusinessToPresentation.Request(i))
                .Select(x => AddDoctorEmail(x))
                .ToList();
        }

        public List<RequestPersonnel> GetDonationCenterRequests()
        {
            Personnel loggedPersonnel = BusinessToPresentation.Personnel(personnelService.GetOne(GetUid()));
            string donationCenterID = loggedPersonnel.DonationCenterID;

            return requestService
                .FindDonationCenterRequests(donationCenterID)
                .AsEnumerable()
                .Select(i => BusinessToPresentation.Request(i))
                .Select(x => AddDoctorEmail(x))
                .ToList();
        }

        public RequestPersonnel AddDoctorEmail(RequestPersonnel req)
        {
            DoctorDisplayData doctor = doctorService
                .GetValidDoctors()
                .AsEnumerable()
                .Select(x => BusinessToPresentation.MapDoctorDisplayData(x))
                .Where(x => x.ID == req.doctorId)
                .Single();
            req.doctorEmail = doctor.EmailAddress;
            req.doctorName = doctor.LastName + " " + doctor.FirstName;
            return req;
        }

        public string GetUid()
        {
            return ((FirebaseAuthLink) Session["authlink"]).User.LocalId;
        }

        public bool IsNotPersonnel()
        {
            return (string) Session["usertype"] != "personnel";
        }
    }
}