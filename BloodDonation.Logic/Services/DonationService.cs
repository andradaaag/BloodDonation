using BloodDonation.Logic.Mappers;
using BloodDonation.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Logic.Models;
using BloodDonation.Utils.Enums;

namespace BloodDonation.Logic.Services
{
    public class DonationService
    {
        private DonationRepository Repository = new DonationRepository();
        private LogicToDataMapperPersonnel LogicToData = new LogicToDataMapperPersonnel();
        private DataToLogicMapperPersonnel DataToLogic = new DataToLogicMapperPersonnel();

        private DonationCenterPersonnelRepository dcprRepo = new DonationCenterPersonnelRepository();

        public List<Donation> FindAll()
        {
            return Repository
                .FindAll()
                .AsEnumerable()
                .Select(i => DataToLogic.Donation(i))
                .ToList();
        }
        public List<Donation> FindByDonationCenter(string donationCenterID)
        {
            return Repository
                .FindByDonationCenter(donationCenterID)
                .AsEnumerable()
                .Select(i => DataToLogic.Donation(i))
                .ToList();
        }

        public List<Donation> FindByDonCenterForCompSep(string UID)
        {
            return Repository
               .FindByDonationCenter(dcprRepo.GetOne(UID).DonationCenterID)
               .AsEnumerable()
               .Select(i => DataToLogic.Donation(i))
               .Where(i => i.Stage == Stage.Sampling || i.Stage == Stage.BiologicalQualityControl)
               .ToList();

        }

        public List<Donation> FindByDonCenterForLabRes(string UID)
        {
            return Repository
               .FindByDonationCenter(dcprRepo.GetOne(UID).DonationCenterID)
               .AsEnumerable()
               .Select(i => DataToLogic.Donation(i))
               .Where(i => i.Stage == Stage.Sampling || i.Stage == Stage.Preparation)
               .ToList();

        }

        public List<Donation> FindUnsolvedByDonationCenter(string donationCenterID)
        {
            return Repository
               .FindByDonationCenter(donationCenterID)
               .AsEnumerable()
               .Select(i => DataToLogic.Donation(i))
               .Where(i => i.Stage != Stage.Redistribution && i.Stage != Stage.Failed)
               .ToList();
        }



        public void Add(Donation d)
        {
            Repository
                .Add(LogicToData.Donation(d));

        }

        public void Edit(Donation d)
        {
            Repository
                .Edit(LogicToData.Donation(d));
        }

        public Donation GetOne(string id)
        {
            return DataToLogic.Donation(Repository.GetOne(id));
        }

        public List<Donation> FindUnsolved()
        {
            return Repository
                .FindUnresolved()
                .AsEnumerable()
                .Select(i => DataToLogic.Donation(i))
                .ToList();
        }
    }
}
