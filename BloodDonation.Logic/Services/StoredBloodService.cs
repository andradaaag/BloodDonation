﻿using BloodDonation.Data.Repositories;
using BloodDonation.Logic.Mappers;
using BloodDonation.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Services
{
    public class StoredBloodService
    {
        private StoredBloodRepository Repository = new StoredBloodRepository();
        private DataToLogicMapperPersonnel DataToLogic = new DataToLogicMapperPersonnel();
        private LogicToDataMapperPersonnel LogicToData = new LogicToDataMapperPersonnel();

        public List<StoredBlood> FindAll(string CollectionCenterID)
        {
            //TODO: implement the proper FINDALL in repo
            return Repository
                .FindAll()
                .AsEnumerable()
                .Select(i=>DataToLogic.StoredBlood(i))
                .ToList();
        }

        public StoredBlood GetOne(string id)
        {
            return DataToLogic.StoredBlood(Repository.GetOne(id));
        }

        public void Add(StoredBlood storedBlood)
        {
            Repository.Add(LogicToData.StoredBlood(storedBlood));
        }

    }
}
