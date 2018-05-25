﻿using BloodDonation.Logic   .Services;
using BloodDonation.Mappers;
using BloodDonation.Models;
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


        public Personnel getLoggedPersonnel()
        {
            // TODO GET CURRENT LOGGED USER
            return BusinessToPresentation.Personnel(personnelService.GetOne("1"));
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
            return View("PersonalDetailsView", getLoggedPersonnel());
        }

        [HttpPost]
        public ActionResult AddDonationInDb(DonationModel donation)
        {
            if (IsNotPersonnel())
                return errorController.Error();
            donation.Stage = "Sampling";
            donation.DonationTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

            donationService.Add(PresentationToBusiness.Donation(donation));
            

            return Success();

        }




        //START SEPARATE COMPONENTS
        public ActionResult SeparateComponents()
        {
            if (IsNotPersonnel())
                return errorController.Error();
            DonationListModel dlm = new DonationListModel
            {
                Donations = GetUnsolvelDonations()
                .AsEnumerable()
                .Where(i => i.Stage == "Sampling" || i.Stage == "BiologicalQualityControl")
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
        public ActionResult SeparateComponentsToDB(DonationModel donation)
        {
            if (IsNotPersonnel())
                return errorController.Error();
            DonationModel original = GetOne(donation.ID);
            original.Plasma = donation.Plasma;
            original.RBC = donation.RBC;
            original.Thrombocytes = donation.Thrombocytes;
            if (original.Stage == "Sampling")
                original.Stage = "Preparation";
            else
            {
                AddComponents(original);
            }

            donationService.Edit(PresentationToBusiness.Donation(original));
            Thread.Sleep(1000);
            return SeparateComponents();
        }
        //END SEPARATE COMPONENTS


        public ActionResult Requests()
        {
            RequestList rl = new RequestList
            {
                Requests = GetAllRequests()
                .AsEnumerable()
                .ToList()
            };
            return View("RequestsView", rl);
        }




        //START LAB RESULTS
        public ActionResult LabResults()
        {
            if (IsNotPersonnel())
                return errorController.Error();
            DonationListModel dlm = new DonationListModel
            {
                Donations = GetUnsolvelDonations()
                .AsEnumerable()
                .Where(i => i.Stage == "Sampling" || i.Stage == "Preparation")
                .ToList()
            };
            return View("LabResultsView", dlm);
        }
       
        [HttpPost]
        public ActionResult EditDonationLab(DonationModel donation)
        {
            if (IsNotPersonnel())
                return errorController.Error();
            donation.ID = "BUHA";
            return View("EditDonationLabView", donation);
        }

        public ActionResult AcceptRequest(string id)
        {
            RequestPersonnel r = BusinessToPresentation.Request(requestService.GetOne(id));
            return View("AcceptRequestView", r);
        }

        public ActionResult ConfirmAcceptRequest(string id)
        {
            requestService.EditStatus(id, PresentationToBusiness.Status(Status.Accepted));
            return Success();
        }

        public ActionResult EditRequest(string id)
        {
            RequestPersonnel r = BusinessToPresentation.Request(requestService.GetOne(id));
            NewStatus ns = new NewStatus();
            ns.ID = r.ID;
            ns.status = r.status.ToString();
            return View("EditRequestView",ns);
        }


        [HttpPost]
        public ActionResult RequestToDb(NewStatus ns)
        {
            Status s = (Status)Enum.Parse(typeof(Status), ns.status);
            requestService.EditStatus(ns.ID, PresentationToBusiness.Status(s));
            return Success();

        }

        [HttpPost]
        public ActionResult UpdatePersonnel(Personnel p)
        {
            personnelService.Edit(PresentationToBusiness.Personnel(p));
            return Success();
        }

        [HttpPost]
        public ActionResult LabResultsToDB(DonationModel donation)
        {

            DonationModel original = GetOne(donation.ID);
            original.Alt = donation.Alt;
            original.HepatitisB = donation.HepatitisB;
            original.HepatitisC = donation.HepatitisC;
            original.Hiv = donation.Hiv;
            original.Htlv = donation.Htlv;
            original.Syphilis = donation.Syphilis;
            if (original.Stage == "Sampling")
                original.Stage = "BiologicalQualityControl";
            else
            {
                AddComponents(original);
            }

            donationService.Edit(PresentationToBusiness.Donation(original));
            Thread.Sleep(1000);
            return LabResults();
        }
        //END LAB RESULTS

        


        //UTIL
        public void AddComponents(DonationModel donation)
        {
            if (donation.isAccepted())
            {
                donation.Stage = "Redistribution";
                StoredBloodModel RBC = new StoredBloodModel
                {
                    Amount = donation.RBC,
                    CollectionDate = donation.DonationTime,
                    Component = "RedBloodCells",
                    BloodTypeGroup = donation.BloodTypeGroup,
                    BloodTypePH = donation.BloodTypeRH
                };
                StoredBloodModel Plasma = new StoredBloodModel
                {
                    Amount = donation.Plasma,
                    CollectionDate = donation.DonationTime,
                    Component = "Plasma",
                    BloodTypeGroup = donation.BloodTypeGroup,
                    BloodTypePH = donation.BloodTypeRH
                };
                StoredBloodModel Thrombocytes = new StoredBloodModel
                {
                    Amount = donation.Thrombocytes,
                    CollectionDate = donation.DonationTime,
                    Component = "Thrombocytes",
                    BloodTypeGroup = donation.BloodTypeGroup,
                    BloodTypePH = donation.BloodTypeRH
                };
                storedBloodService.Add(PresentationToBusiness.StoredBlood(RBC));
                storedBloodService.Add(PresentationToBusiness.StoredBlood(Plasma));
                storedBloodService.Add(PresentationToBusiness.StoredBlood(Thrombocytes));
            }
            else
            {
               donation.Stage = "Failed";
            }
        }

        public List<RequestPersonnel> GetAllRequests()
        {
            return requestService
                .FindAll()
                .AsEnumerable()
                .Select(i => BusinessToPresentation.Request(i))
                .ToList();
        }

        public List<DonationModel> GetAllDonations()
        {
            return donationService
                .FindAll()
                .AsEnumerable()
                .Select(i => BusinessToPresentation.Donation(i))
                .ToList();
        }

        public List<DonationModel> GetUnsolvelDonations()
        {
            return donationService
                .FindUnsolved()
                .AsEnumerable()
                .Select(i => BusinessToPresentation.Donation(i))
                .ToList();
        }

        public DonationModel GetOne(string id)
        {
            return BusinessToPresentation.Donation(donationService.GetOne(id));
        }

        public string GetUid()
        {
            return "-LCmdpPObpuHY0Hp0VNH";
        }

        public bool IsNotPersonnel() => userService.GetRole(GetUid()) != "personnel";

    }
}