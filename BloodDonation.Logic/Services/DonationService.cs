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
        private DataToLogicMapperDonor DataToLogicDonor = new DataToLogicMapperDonor();

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

        public List<DonationDetails> FindDonationsByDonorCNP(string donorCNP)
        {
            return Repository
               .FindByDonorCNP(donorCNP)
               .AsEnumerable()
               .Select(i => DataToLogicDonor.MapDonationToDonationDetails(i))
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

        public List<String> showAvailableHours(String date)
        {
            List<String> mylist = new List<string>(new String[] { "07:00"});
            DateTime myDate = DateTime.ParseExact("07:00", "HH:mm",
                                       System.Globalization.CultureInfo.InvariantCulture);
            for(int i = 1; i < 7; i++)
            {
                DateTime d = myDate.AddHours(i);
                if (isAvailable(d.ToString("HH:mm"), date))
                {
                    mylist.Add(d.ToString("HH:mm"));
                }
            }
            return mylist;
        }


        private bool isAvailable(String hour, String date)
        {
            return ! Repository.GetBookedHours(date).Contains(hour);
        }
    }
}
