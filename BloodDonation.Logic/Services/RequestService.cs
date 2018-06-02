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
        private RequestRepository reqRepo = new RequestRepository();
        private DonationCenterPersonnelRepository persRepo = new DonationCenterPersonnelRepository();


        //Get sorted list of existing stored blood. Only needed blood type. Ordered Ascending by time (oldest blood first)
        public List<StoredBlood> GetCompatibleBlood(string donationCenterID, BloodType blood,Component component)
        {
            return bloodRepo
                    .FindAllByDonationCenter(donationCenterID)
                    .AsEnumerable()
                    .Select(x => DataToLogic.StoredBlood(x))
                    .Where(storedBlood => blood.bloodComponent == component)
                    .Where(storedBlood => blood.isCompatible(storedBlood.BloodType))
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

        public void BloodRequestCompleteRequest(string donationCenterID, RequestPersonnel bloodRequest)
        {
            List<Logic.Models.StoredBlood>  usedBlood =
                 GetCompatibleBlood(donationCenterID, bloodRequest.bloodType,bloodRequest.bloodType.bloodComponent);
            BloodRequestUpdateExistingBlood(usedBlood, bloodRequest.quantity);
        }


        public List<StoredBlood> AcceptRequest(RequestPersonnel bloodRequest, string UID)
        {
            DonationCenterPersonnel loggedPersonnel = DataToLogic.Personnel(persRepo.GetOne(UID));
            string donationCenterID = loggedPersonnel.DonationCenterID;
            List<StoredBlood> usedBlood = GetCompatibleBlood(donationCenterID, bloodRequest.bloodType,bloodRequest.bloodType.bloodComponent);
            return usedBlood;
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
