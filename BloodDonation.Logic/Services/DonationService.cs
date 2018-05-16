using BloodDonation.Logic.Mappers;
using BloodDonation.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Logic.Models;

namespace BloodDonation.Logic.Services
{
    public class DonationService
    {
        private DonationRepository Repository = new DonationRepository();
        private LogicToDataMapperPersonnel LogicToData = new LogicToDataMapperPersonnel();
        private DataToLogicMapperPersonnel DataToLogic = new DataToLogicMapperPersonnel();

        public async Task<List<Donation>> FindAll()
        {
            List<Data.Models.Donation> l = await Repository.FindAll();
            List<Donation> l2 = new List<Donation>();
            foreach(var i in l)
            {
                l2.Add(DataToLogic.Donation(i));
            }
            return l2;
        }

        public async Task Add(Donation d)
        {
            await Repository.Add(LogicToData.Donation(d));

        }
    }
}
