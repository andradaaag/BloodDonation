using BloodDonation.Business.Mappers;
using BloodDonation.Data.Repositories;
using BloodDonation.Logic.Mappers;
using BloodDonation.Logic.Models;
using BloodDonation.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BloodDonation.Logic.Services
{
    public class PersonnelService
    {
        private readonly DonationCenterPersonnelRepository Repository = new DonationCenterPersonnelRepository();
        private readonly LogicToDataMapperPersonnel logicToData = new LogicToDataMapperPersonnel();
        private readonly DataToLogicMapperPersonnel dataToLogic = new DataToLogicMapperPersonnel();

        private readonly DonationRepository donRepo = new DonationRepository();
        private readonly DonationCenterPersonnelRepository dcprRepo = new DonationCenterPersonnelRepository();
        private readonly StoredBloodRepository bloodRepo = new StoredBloodRepository();

        

        private readonly DateTime epoch = new DateTime(1970, 1, 1);
        public List<AccountRequest> GetPersonnelAccountRequests()
        {
            // TO DO
            return null;
        }

        

        public void AddDonationInDB(Donation donation, string UID, bool keepWhole)
        {
            donation.Stage = keepWhole? Stage.Preparation : Stage.Sampling;
            donation.DonationCenterId = dcprRepo.GetOne(UID).DonationCenterID;
            donation.DonationTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            donation.RBC = keepWhole ? -1:0;
            donRepo.Add(logicToData.Donation(donation));

      

        }

        public void SeparateComponentsFromDonation(Donation donation)
        {
            
            Donation original = dataToLogic.Donation(donRepo.GetOne(donation.ID));
            original.Plasma = donation.Plasma;
            original.RBC = donation.RBC;
            original.Thrombocytes = donation.Thrombocytes;
            if (original.Stage == Stage.Sampling)
                original.Stage = Stage.Preparation;
            else
            {
                AddComponentsFromDonation(original);
            }

            donRepo.Edit(logicToData.Donation(original));
        }

        public void SeparateComponentsFromBlood(SeparateBlood blood)
        {
            StoredBlood original = dataToLogic.StoredBlood(bloodRepo.GetOne(blood.ID));
            blood.DonationCenterID = original.DonationCenterID;
            blood.BloodType = original.BloodType;
            blood.CollectionDate = original.CollectionDate;

            AddComponentsFromBlood(blood);
        }

        public void CommitLabResults(Donation don)
        {
            Donation original = dataToLogic.Donation(donRepo.GetOne(don.ID));
            original.BloodType = don.BloodType;
            original.Alt = don.Alt;
            original.HepatitisB = don.HepatitisB;
            original.HepatitisC = don.HepatitisC;
            original.Hiv = don.Hiv;
            original.Htlv = don.Htlv;
            original.Syphilis = don.Syphilis;
            if (original.Stage == Stage.Sampling)
                original.Stage = Stage.BiologicalQualityControl;
            else
            {
                AddComponentsFromDonation(original);
            }
            donRepo.Edit(logicToData.Donation(original));

        }

        private StoredBlood CompFromDonation(Donation donation, Component comp,string centerId, int amount)
        {

            return new StoredBlood
            {
                Amount = amount,
                CollectionDate = donation.DonationTime,
                Component = comp,
                BloodType = new BloodType
                {
                    Group = donation.BloodType.Group,
                    RH = donation.BloodType.RH
                },
                DonationCenterID = centerId
            };
        }

        private StoredBlood CompFromBlood(SeparateBlood blood, Component comp, string centerId, int amount)
        {

            return new StoredBlood
            {
                Amount = amount,
                CollectionDate = blood.CollectionDate,
                Component = comp,
                BloodType = new BloodType
                {
                    Group = blood.BloodType.Group,
                    RH = blood.BloodType.RH
                },
                DonationCenterID = centerId
            };
        }

        private void AddComponentsFromDonation(Donation donation)
        {
            if (donation.IsAccepted())
            {
                string centerId = donation.DonationCenterId;
                donation.Stage =Stage.Redistribution;
                if (donation.RBC == -1)
                {
                    StoredBlood whole = CompFromDonation(donation, Component.Whole, centerId, 400);
                    for (int i =0; i<whole.Amount; i++)
                        bloodRepo.Add(logicToData.StoredBlood(whole));
                }
                else
                {
                    StoredBlood RBC = CompFromDonation(donation, Component.RedBloodCells, centerId, donation.RBC);
                    StoredBlood Plasma = CompFromDonation(donation, Component.Plasma, centerId, donation.Plasma);
                    StoredBlood Thrombocytes = CompFromDonation(donation, Component.Thrombocytes, centerId, donation.Thrombocytes);
                    bloodRepo.Add(logicToData.StoredBlood(RBC));
                    bloodRepo.Add(logicToData.StoredBlood(Plasma));
                    bloodRepo.Add(logicToData.StoredBlood(Thrombocytes));
                }
            }
            else
            {
                donation.Stage = Stage.Failed;
            }
        }
        private void AddComponentsFromBlood(SeparateBlood blood)
        {
            
            string centerId = blood.DonationCenterID;
            StoredBlood RBC = CompFromBlood(blood, Component.RedBloodCells, centerId, blood.RBC);
            StoredBlood Plasma = CompFromBlood(blood, Component.Plasma, centerId, blood.Plasma);
            StoredBlood Thrombocytes = CompFromBlood(blood, Component.Thrombocytes, centerId,  blood.Thrombocytes);
            bloodRepo.Add(logicToData.StoredBlood(RBC));
            bloodRepo.Add(logicToData.StoredBlood(Plasma));
            bloodRepo.Add(logicToData.StoredBlood(Thrombocytes));
            bloodRepo.DeleteById(blood.ID);
            
        }

        public List<DonationCenterPersonnel> FindAll()
        {
            List<DonationCenterPersonnel> toReturn = new List<DonationCenterPersonnel>();
            foreach (Data.Models.DonationCenterPersonnel p in Repository.findAll())
            {
                toReturn.Add(dataToLogic.Personnel(p));
            }
            return toReturn;
        }

        public void Add(DonationCenterPersonnel p)
        {

            Repository.Save(logicToData.Personnel(p));

        }

        public void Edit(DonationCenterPersonnel p)
        {
            Repository.Edit(logicToData.Personnel(p));
        }

        public DonationCenterPersonnel GetOne(string id)
        {
            return dataToLogic.Personnel(Repository.GetOne(id));
        }
    }
}
