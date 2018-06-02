using BloodDonation.Logic   .Services;
using BloodDonation.Mappers;
using BloodDonation.Models;
using BloodDonation.Utils.Enums;
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

        private ErrorController errorController = new ErrorController();

        private BusinessToPresentationMapperPersonnel BusinessToPresentation = new BusinessToPresentationMapperPersonnel();
        private PresentationToBusinessMapperPersonnel PresentationToBusiness = new PresentationToBusinessMapperPersonnel();

        private ActionResult goIfPossible(ActionResult actionResultSuccess)
        {
            if (Session["usertype"] == null)
                return RedirectToAction("Index", "Login");
            if ((string)Session["usertype"] != "personnel")
                return RedirectToAction("Error", "Error");
            if (Session["authlink"] != null && ((FirebaseAuthLink)Session["authlink"]).IsExpired())
                return RedirectToAction("Index", "Login");

            return actionResultSuccess;
        }

        public ActionResult Index()
        {
            
            ///CRISTI LOG EXPIRED BLOOD phase 1 at init check if there is expired blood
            List<BloodDonation.Logic.Models.StoredBlood> expiredBlood = personnelService.GetExpiredBlood();
            if(expiredBlood.Count() > 0)
            {
                return goIfPossible(View("DeleteExpiredBloodView",expiredBlood));
            }

            return goIfPossible(View("AddDonationView"));
        }

        public ActionResult Success()
        {
            return goIfPossible(View("SuccessView"));
        }

        public ActionResult AddDonation()
        {

            return goIfPossible(View("AddDonationView"));
        }

        public ActionResult Home()
        {
            return Index();
        }

        public ActionResult PersonalDetails()
        {
            return goIfPossible(View("PersonalDetailsView", BusinessToPresentation.Personnel( personnelService.GetOne(GetUid()))));
        }
        

        [HttpPost]
        public ActionResult AddDonationInDb(DonationModel donation)
        {
         
            personnelService.AddDonationInDB(PresentationToBusiness.Donation(donation), GetUid(),donation.KeepWhole);
            return Success();
        }

        //START SEPARATE COMPONENTS
        public ActionResult SeparateComponents()
        {
            
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
            return goIfPossible(View("SeparateComponentsView", dlm));
        }

        [HttpPost]
        public ActionResult EditDonationSeparation(DonationModel donation)
        {
          
            return goIfPossible(View("EditDonationSeparation", donation));
        }

        [HttpPost]
        public ActionResult EditBloodSeparation(StoredBloodModel stored)
        {
           
            SeparateStoredBloodModel blood = new SeparateStoredBloodModel
            {
                ID = stored.ID,
                DonationCenterID = stored.DonationCenterID,
                BloodTypeGroup = stored.BloodTypeGroup,
                BloodTypeRH = stored.BloodTypeRH,
                CollectionDate = (stored.CollectionDate - new DateTime(1970, 1, 1)).Seconds,
                DonorEmail = stored.DonnorEmail
                
            };
            return goIfPossible(View("EditBloodSeparation", blood));
        }

        [HttpPost]
        public ActionResult SeparateComponentsToDB(DonationModel donation)
        {
         
            personnelService.SeparateComponentsFromDonation(PresentationToBusiness.Donation(donation));
            Thread.Sleep(1000);
            return SeparateComponents();
        }

        [HttpPost]
        public ActionResult SeparateBloodComponentsToDB(SeparateStoredBloodModel storedBlood)
        {
           
            personnelService.SeparateComponentsFromBlood(PresentationToBusiness.SeparateBlood(storedBlood));
            Thread.Sleep(1000);
            return SeparateComponents();
        }
        //END SEPARATE COMPONENTS

       

        //START LAB RESULTS
        public ActionResult LabResults()
        {
           
            DonationListModel dlm = new DonationListModel
            {
                Donations = donationService
                .FindByDonCenterForLabRes(GetUid())
                .AsEnumerable()
                .Select(i=>BusinessToPresentation.Donation(i))
                .ToList()
            };
            return goIfPossible(View("LabResultsView", dlm));
        }
       
        [HttpPost]
        public ActionResult EditDonationLab(DonationModel donation)
        {
           
            return goIfPossible(View("EditDonationLabView", donation));
        }

        [HttpPost]
        public ActionResult LabResultsToDB(DonationModel donation)
        {
          
            personnelService.CommitLabResults(PresentationToBusiness.Donation(donation));

            Thread.Sleep(1000);
            return LabResults();
        }
        //END LAB RESULTS

        public ActionResult AcceptRequest(string id)
        {
            List<Logic.Models.StoredBlood> usedBlood = new List<Logic.Models.StoredBlood>();

            RequestPersonnel bloodRequest = BusinessToPresentation.Request(requestService.GetOne(id));
            Personnel loggedPersonnel = BusinessToPresentation.Personnel(personnelService.GetOne(GetUid()));
            string donationCenterID = loggedPersonnel.DonationCenterID;

            int missingBlood = requestService.BloodRequestGetUsedBlood(donationCenterID, requestService.GetOne(id), ref usedBlood);

            if (missingBlood <0)
            {
                String param = Convert.ToString( -missingBlood);
                return goIfPossible(View("MissingBloodView", model: param));
            }

            bloodRequest = AddDoctorEmail(bloodRequest);
            bloodRequest.quantityString = Convert.ToString(bloodRequest.quantity);
            return goIfPossible( View( "AcceptRequestView", new TransferObjectForAcceptView(usedBlood, bloodRequest) ) );
        }

        ///CRISTI LOG EXPIRED BLOOD phase 4 get again all expired blood & delete it by ID
        public ActionResult DeleteExpiredBlood()
        {
            personnelService.GetExpiredBlood()
                           .ForEach(el => storedBloodService.RemoveBloodById(el.ID));
            return Index();
            
        }


        public ActionResult ConfirmAcceptRequest(string id)
        {
            RequestPersonnel bloodRequest = BusinessToPresentation.Request(requestService.GetOne(id));
            Personnel loggedPersonnel = BusinessToPresentation.Personnel(personnelService.GetOne(GetUid()));
            string donationCenterID = loggedPersonnel.DonationCenterID;

            requestService.EditStatus(id, Status.Accepted);
            requestService.BloodRequestCompleteRequest(donationCenterID, requestService.GetOne(id));

            requestService.EditSource(id, donationCenterID);

            return Success();
        }

        public ActionResult SendRequest(string id)
        {
            EmailServiceBloodDonation mail = new EmailServiceBloodDonation();
            Logic.Models.RequestPersonnel r = requestService.GetOne(id);
            r.status = Status.OnTheWay;
            requestService.Edit(r);
            String docMail = doctorService.findById(r.doctorId).emailAddress;

            Thread th = new Thread(() => mail.ComposeDoctorMail(docMail, r.ID, r.source));
            th.Start();
            
                
            return goIfPossible(View("PendingRequestsView", GetDonationCenterRequests()));
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
        
        public ActionResult ViewStoredBlood()
        {
            BloodAmounts blood = BusinessToPresentation.BloodAmounts(personnelService.GetArrayOfBloodQuantity(GetUid()));
            return goIfPossible(View("StoredBloodView", blood));
        }
        public ActionResult AcceptedRequests()
        {
            List<RequestPersonnel> listOfAcceptedRequest = GetDonationCenterRequests();
            listOfAcceptedRequest.Sort((el1, el2) =>(-1) *  el1.urgency.CompareTo(el2.urgency));

            return goIfPossible(View("PendingRequestsView", listOfAcceptedRequest));
        }

        public ActionResult PendingRequests()
        {
            List<RequestPersonnel> listOfUnresolvedRequest = GetUnsolvedRequests();
            listOfUnresolvedRequest.Sort((el1, el2) => (-1) * el1.urgency.CompareTo(el2.urgency));

            return goIfPossible(View("PendingRequestsView", listOfUnresolvedRequest));
        }

        public ActionResult Requests()
        {
            return goIfPossible(View("RequestsView"));
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