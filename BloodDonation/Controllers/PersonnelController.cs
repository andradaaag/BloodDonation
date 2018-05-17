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
                if (donation.isAccepted())
                {
                    original.Stage = "Redistribution";
                    StoredBloodModel RBC = new StoredBloodModel
                    {
                        Amount = original.RBC,
                        CollectionDate = original.DonationTime,
                        Component = "RedBloodCells",
                        BloodTypeGroup = original.BloodTypeGroup,
                        BloodTypePH = original.BloodTypePH
                    };
                    StoredBloodModel Plasma = new StoredBloodModel
                    {
                        Amount = original.Plasma,
                        CollectionDate = original.DonationTime,
                        Component = "Plasma",
                        BloodTypeGroup = original.BloodTypeGroup,
                        BloodTypePH = original.BloodTypePH
                    };
                    StoredBloodModel Thrombocytes = new StoredBloodModel
                    {
                        Amount = original.Thrombocytes,
                        CollectionDate = original.DonationTime,
                        Component = "Thrombocytes",
                        BloodTypeGroup = original.BloodTypeGroup,
                        BloodTypePH = original.BloodTypePH
                    };
                    storedBloodService.Add(PresentationToBusiness.StoredBlood(RBC));
                    storedBloodService.Add(PresentationToBusiness.StoredBlood(Plasma));
                    storedBloodService.Add(PresentationToBusiness.StoredBlood(Thrombocytes));
                }
            }

            donationService.Edit(PresentationToBusiness.Donation(original));
            Thread.Sleep(1000);
            return SeparateComponents();
        }
        //END SEPARATE COMPONENTS

        
        //UTIL
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