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
    public class RequestService
    {

        private RequestRepository Repository = new RequestRepository();
        private LogicToDataMapperPersonnel LogicToData = new LogicToDataMapperPersonnel();
        private DataToLogicMapperPersonnel DataToLogic = new DataToLogicMapperPersonnel();



        public int GetCompatibleStoredBlood(string donationCenterID, BloodType bloodType, RequestComponent component)
        {
            
            StoredBloodRepository bloodRepo = new StoredBloodRepository();
            List<StoredBlood> bloodList = bloodRepo
                    .FindAllByDonationCenter(donationCenterID)
                    .AsEnumerable()
                    .Select(x => DataToLogic.StoredBlood(x))
                    .ToList();

            int totalQuantity = 0;
            foreach (StoredBlood blood in bloodList)
                if(DataToLogic.RequestComponent(blood.Component) == component && blood.BloodType.CanDonate(bloodType))
                    totalQuantity += blood.Amount;

            return totalQuantity;
        }

        public int GetMissingBlood(string donationCenterID, RequestPersonnel r)
        {
            int storedBlood = this.GetCompatibleStoredBlood(donationCenterID,r.bloodType,r.component);

            if (storedBlood >= r.quantity)
                return 0;
            return r.quantity - storedBlood;
        }

        public List<RequestPersonnel> FindAll()
        {
            return Repository
                .FindAll()
                .AsEnumerable()
                .Select(i => DataToLogic.Request(i))
                .ToList();
        }

        public List<RequestPersonnel> FindUnsolved()
        {
            return Repository
                .GetUnsolvedRequests()
                .AsEnumerable()
                .Select(i => DataToLogic.Request(i))
                .ToList();
        }

        public List<RequestPersonnel> FindDonationCenterRequests(string donationCenterID)
        {
            return Repository
                .GetRequestByDonationCenter(donationCenterID)
                .AsEnumerable()
                .Select(i => DataToLogic.Request(i))
                .ToList();
        }



        public void Edit(RequestPersonnel req)
        {
            Repository.Edit(LogicToData.RequestPersonelToRequest(req));
        }


        public void EditStatus(string id,Status s)
        {
            if(s == Status.Accepted)


            Repository.EditStatus(id, LogicToData.Status(s));
        }

        public void EditSource(string id, string donationCenterID)
        {
            Repository
                .EditSource(id, donationCenterID);
        }

        public RequestPersonnel GetOne(string id)
        {
            return DataToLogic.Request(Repository.GetOne(id));
        }

        public List<RequestPersonnel> FindByDoctorId(string id)
        {
            return Repository
                   .GetRentalByDoctorId(id)
                   .AsEnumerable()
                   .Select(i => DataToLogic.Request(i))
                   .ToList();
        }

        public void AddRequest(RequestPersonnel request)
        {
            Repository.Save(LogicToData.RequestPersonelToRequest(request));
        }
    }
}
