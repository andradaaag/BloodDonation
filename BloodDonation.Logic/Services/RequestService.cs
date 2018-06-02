using BloodDonation.Logic.Mappers;
using BloodDonation.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Logic.Models;
using BloodDonation.Utils.Enums;
using System.Threading;

namespace BloodDonation.Logic.Services
{
    public class RequestService
    {

        private RequestRepository Repository = new RequestRepository();
        private LogicToDataMapperPersonnel LogicToData = new LogicToDataMapperPersonnel();
        private DataToLogicMapperPersonnel DataToLogic = new DataToLogicMapperPersonnel();
        private StoredBloodRepository bloodRepo = new StoredBloodRepository();


        //Get sorted list of existing stored blood. Only needed blood type. Ordered Ascending by time (oldest blood first)
        public List<Logic.Models.StoredBlood> BloodRequestGetOrderedListOfStoredBloodForBloodType(string donationCenterID, BloodType requiredBlood)
        {
            return bloodRepo
                    .FindAllByDonationCenter(donationCenterID)
                    .AsEnumerable()
                    .Select(x => DataToLogic.StoredBlood(x))
                    .Where(storedBlood => storedBlood.BloodType.Group == requiredBlood.Group &&
                                          storedBlood.BloodType.RH == requiredBlood.RH)
                    .OrderBy(storedBlood => storedBlood.CollectionDate)
                    .ToList();
        }

        //The existing quantity of blood
        public int BloodRequestGetAmountOfBlood(List<Logic.Models.StoredBlood> bloodList)
        {
            return bloodList
                    .AsEnumerable()
                    .Select(storedBlood => storedBlood.Amount)
                    .Sum();
        }

        //Remove / update blood after the request if fulfiled
        public void BloodRequestUpdateExistingBlood(List<Logic.Models.StoredBlood> bloodList, int quantity)
        {
            EmailServiceBloodDonation emailService = new EmailServiceBloodDonation();
            int remainingQuantity = quantity;
            bloodList.ForEach(
                    storedBlood =>
                    {
                        if (remainingQuantity > 0)
                        {
                            if (storedBlood.Amount > remainingQuantity)
                            {
                                storedBlood.Amount -= remainingQuantity;
                                bloodRepo.Edit(LogicToData.StoredBlood(storedBlood));
                                remainingQuantity = 0;
                                Thread sendMail = new Thread(() => emailService.ComposeGuestMail(storedBlood.DonorEmail, storedBlood.DonationCenterID, storedBlood.ID));
                                sendMail.Start();
                            }
                            else
                            {
                                remainingQuantity -= storedBlood.Amount;
                                bloodRepo.DeleteById(storedBlood.ID);
                                Thread sendMail = new Thread(() => emailService.ComposeGuestMail(storedBlood.DonorEmail, storedBlood.DonationCenterID, storedBlood.ID));
                                sendMail.Start();
                            }
                        }
                    }
                );
        }

        public void BloodRequestCompleteRequest(string donationCenterID, Models.RequestPersonnel bloodRequest)
        {
            List<Logic.Models.StoredBlood>  usedBlood =
                 BloodRequestGetOrderedListOfStoredBloodForBloodType(donationCenterID, bloodRequest.bloodType);
            BloodRequestUpdateExistingBlood(usedBlood, bloodRequest.quantity);
        }

        //Manages the whole send blood transaction
        public int BloodRequestGetUsedBlood(string donationCenterID, Models.RequestPersonnel bloodRequest, ref List<Logic.Models.StoredBlood> usedBlood)
        {
            usedBlood = 
                BloodRequestGetOrderedListOfStoredBloodForBloodType(donationCenterID,bloodRequest.bloodType);

            int diff = BloodRequestGetAmountOfBlood(usedBlood) - bloodRequest.quantity;
            return diff;
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
                .GetRequestsInProgress(donationCenterID)
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


            Repository.EditStatus(id, s);
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

        public void DeleteById(String Id)
        {
            Repository.DeleteById(Id);
        }

        public void AddRequest(RequestPersonnel request)
        {
            Repository.Save(LogicToData.RequestPersonelToRequest(request));
        }
    }
}
