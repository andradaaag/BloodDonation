﻿using BloodDonation.Logic.Mappers;
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

        public List<Donation> FindAll()
        {
            return Repository
                .FindAll()
                .AsEnumerable()
                .Select(i => DataToLogic.Donation(i))
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

        public  List<Donation> FindUnsolved()
        {
            return Repository
                .FindUnresolved()
                .AsEnumerable()
                .Select(i=>DataToLogic.Donation(i))
                .ToList();
        }
    }
}