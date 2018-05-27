using BloodDonation.Business.Mappers;
using BloodDonation.Data.Repositories;
using BloodDonation.Logic.Mappers;
using BloodDonation.Data.Models;
using BloodDonation.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Services
{
    public class DonationCenterPersonnelService
    {
        private readonly DonationCenterPersonnelRepository donationCenterPersonnelRepository = new DonationCenterPersonnelRepository();
        private readonly LogicToDataMapper logicToDataMapper = new LogicToDataMapper();
        private readonly DataToLogicMapper dataToLogicMapper = new DataToLogicMapper();

        public bool IsIDPresent(string id)
        {
            return donationCenterPersonnelRepository.IsIDPresent(id);
        }

        public void AddDonationCenterPersonnelAccount (NewUserTransferObject nuto)
        {
            BloodDonation.Data.Models.DonationCenterPersonnel dcp = logicToDataMapper.MapNewDonationCenterPersonnel(nuto);
            donationCenterPersonnelRepository.Save(dcp);
        }

        public List<AccountRequest> GetDonationCenterPersonnelAccountRequests()
        {
            List<Data.Models.DonationCenterPersonnel> myDonationCenterPersonnel = donationCenterPersonnelRepository.findAll();
            List<AccountRequest> myDonationCenterPersonnelAccountRequests = new List<AccountRequest>();
            
            foreach(Data.Models.DonationCenterPersonnel dcp in myDonationCenterPersonnel)
            {
                if (!dcp.isValidAccount())
                {
                    myDonationCenterPersonnelAccountRequests.Add(dataToLogicMapper.MapDonationCenterPersonnelToAccountRequest(dcp));
                }
            }
            return myDonationCenterPersonnelAccountRequests;
        }

        public List<DonationCenterPersonnelTransferObject> GetValidDonationCenterPersonnel()
        {
            List<Data.Models.DonationCenterPersonnel> donationCenterPersonnels = donationCenterPersonnelRepository.findAll();
            List<DonationCenterPersonnelTransferObject> donationCenterPersonnelTransferObjects = new List<DonationCenterPersonnelTransferObject>();

            foreach (Data.Models.DonationCenterPersonnel dcp in donationCenterPersonnels)
            {
                if (dcp.isValidAccount())
                {
                    donationCenterPersonnelTransferObjects.Add(dataToLogicMapper.MapDonationCenterPersonnelTransferObject(dcp));
                }
            }
            return donationCenterPersonnelTransferObjects;
        }

        public List<DonationCenterPersonnelTransferObject> FilterDonationCenterPersonnelBySearchQuery(string searchQuery)
        {
            if (searchQuery.Length == 0)
                return GetValidDonationCenterPersonnel();

            List<DonationCenterPersonnelTransferObject> donationCenterPersonnelTransferObjects = GetValidDonationCenterPersonnel();
            List<DonationCenterPersonnelTransferObject> respone = new List<DonationCenterPersonnelTransferObject>();

            foreach (DonationCenterPersonnelTransferObject dto in donationCenterPersonnelTransferObjects)
            {
                if (dto.FirstName.Contains(searchQuery) || dto.LastName.Contains(searchQuery))
                    respone.Add(dto);
            }

            return respone;
        }

        public void ApproveAccount(string id)
        {
            Data.Models.DonationCenterPersonnel dcp = donationCenterPersonnelRepository.GetOne(id);
            dcp.validateAccount();
            donationCenterPersonnelRepository.Save(dcp);

        }

        public void DeleteAccount(string id)
        {
            Data.Models.DonationCenterPersonnel dcp = donationCenterPersonnelRepository.GetOne(id);
            dcp.invalidateAccount();
            donationCenterPersonnelRepository.Save(dcp);
        }

    }
}