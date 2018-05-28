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



        public int getStoredBlood(string donationCenterID, BloodType bloodType)
        {
            
            StoredBloodRepository bloodRepo = new StoredBloodRepository();
            List<StoredBlood> bloodList = bloodRepo
                    .FindAllByDonationCenter(donationCenterID)
                    .AsEnumerable()
                    .Select(x => DataToLogic.StoredBlood(x))
                    .ToList();


            int totalQuantity = 0;
            foreach (StoredBlood blood in bloodList)
                if(blood.BloodType.isCompatible(bloodType))
                    totalQuantity += blood.Amount;

            return totalQuantity;
        }

        public int getPromisedBlood(string donationCenterID, BloodType bloodType)
        {


            List<RequestPersonnel> promisedRequests = this.FindDonationCenterRequests(donationCenterID);

            int totalQuantity = 0;
            foreach(RequestPersonnel request in promisedRequests)
                if( request.status != Status.Denied && bloodType.isCompatible(request.bloodType))
                    totalQuantity += request.quantity;

            return totalQuantity;
        }

        public int getMissingBlood(string donationCenterID, RequestPersonnel r)
        {

            int storedBlood = this.getStoredBlood(donationCenterID,r.bloodType);
            int promisedBlood = this.getPromisedBlood(donationCenterID, r.bloodType);
            int availableBlood = storedBlood - promisedBlood;

            if (availableBlood >= r.quantity)
                return 0;

            return r.quantity - availableBlood;

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

        public void EditStatus(string id,Status s)
        {
            Repository
                .EditStatus(id, LogicToData.Status(s));
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
