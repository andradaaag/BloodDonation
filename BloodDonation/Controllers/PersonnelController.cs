using BloodDonation.Logic.Services;
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
        private StoredBloodService storedBloodService = new StoredBloodService();

        private BusinessToPresentationMapperPersonnel BusinessToPresentation = new BusinessToPresentationMapperPersonnel();
        private PresentationToBusinessMapperPersonnel PresentationToBusiness = new PresentationToBusinessMapperPersonnel();

        public ActionResult Index()
        {
            //List<Logic.Models.Donation> l = donationService.FindUnsolved();
            return View("AddDonationView");
        }

        //START ADD DONATION

        public ActionResult AddDonation()
        {
            return View("AddDonationView");
        }

        [HttpPost]
        public ActionResult AddDonationInDb(DonationModel donation)
        {
            donation.Stage = "Sampling";
            donation.DonationTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

            donationService.Add(PresentationToBusiness.Donation(donation));
            
            return Index();
        }
        //END ADD DONATION

        //START SEPARATE COMPONENTS
        public ActionResult SeparateComponents()
        {
            DonationListModel dlm = new DonationListModel
            {
                Donations = GetUnsolvelDonations()
                .AsEnumerable()
                .Where(i => i.Stage == "Sampling" || i.Stage == "BiologicalQualityControl")
                .ToList()
            };
            return View("SeparateComponentsView", dlm);
        }

        //TODO: find a way to make this post
        public ActionResult EditDonationSeparation(DonationModel donation)
        {
            return View("EditDonationSeparation", donation);
        }

        [HttpPost]
        public ActionResult SeparateComponentsToDB(DonationModel donation)
        {
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


        //START LAB RESULTS
        public ActionResult LabResults()
        {
            DonationListModel dlm = new DonationListModel
            {
                Donations = GetUnsolvelDonations()
                .AsEnumerable()
                .Where(i => i.Stage == "Sampling" || i.Stage == "Preparation")
                .ToList()
            };
            return View("LabResultsView", dlm);
        }

        //TODO: find a way to make this post
        public ActionResult EditDonationLab(DonationModel donation)
        {
            return View("EditDonationLabView", donation);
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
                    BloodTypePH = donation.BloodTypePH
                };
                StoredBloodModel Plasma = new StoredBloodModel
                {
                    Amount = donation.Plasma,
                    CollectionDate = donation.DonationTime,
                    Component = "Plasma",
                    BloodTypeGroup = donation.BloodTypeGroup,
                    BloodTypePH = donation.BloodTypePH
                };
                StoredBloodModel Thrombocytes = new StoredBloodModel
                {
                    Amount = donation.Thrombocytes,
                    CollectionDate = donation.DonationTime,
                    Component = "Thrombocytes",
                    BloodTypeGroup = donation.BloodTypeGroup,
                    BloodTypePH = donation.BloodTypePH
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

    }
}