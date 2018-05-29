using BloodDonation.Data.Repositories;
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

        private readonly DonationCenterPersonnelRepository dcprRepo = new DonationCenterPersonnelRepository();


        public List<StoredBlood> FindAll(string CollectionCenterID)
        {
            //TODO: implement the proper FINDALL in repo
            return Repository
                .FindAll()
                .AsEnumerable()
                .Select(i=>DataToLogic.StoredBlood(i))
                .ToList();
        }

        public List<StoredBlood> GetWholeStoredBloodByDonCent(string UID)
        {
            return Repository
                .FindAllByDonationCenter(dcprRepo.GetOne(UID).DonationCenterID)
                .Select(i => DataToLogic.StoredBlood(i))
                .Where(i => i.Component == Data.Models.Component.Whole)
                .ToList();
        }

        public List<StoredBlood> GetStoredBloodByDonationCenter(string donationCenterID)
        {
            return Repository
                .FindAllByDonationCenter(donationCenterID)
                .AsEnumerable()
                .Select(x => DataToLogic.StoredBlood(x))
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


        public void RemoveBlood(string donationCenterID,int quantity, BloodType receiverBloodType, RequestComponent component)
        {
            List<StoredBlood> storedBloodList = GetStoredBloodByDonationCenter(donationCenterID);
            //storedBloodList.Sort();

            int i = 0;
            int a = 0;
            while( i < storedBloodList.Count && quantity > 0)
            {
                StoredBlood current = storedBloodList[i];
                if (DataToLogic.RequestComponent(current.Component) == component)
                {
                    a = 1;
                    if (current.BloodType.CanDonate(receiverBloodType))
                    {
                        a = 2;
                        if (current.Amount < quantity)
                        {
                            quantity = quantity - current.Amount;
                            current.Amount = 0;
                        }
                        else
                        {
                            current.Amount = current.Amount - quantity;
                            quantity = 0;
                        }
                        Repository.Edit(LogicToData.StoredBlood(current));
                    }
                }
                i++;    
            }
        }
    }
}
