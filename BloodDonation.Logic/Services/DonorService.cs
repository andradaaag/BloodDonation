using System;
using System.Collections.Generic;
using BloodDonation.Data.Repositories;
using BloodDonation.Business.Mappers;
using BloodDonation.Business.Models;
using BloodDonation.Data.Models;
using BloodDonation.Logic.Mappers;
using BloodDonation.Logic.Models;

namespace BloodDonation.Business.Services
{
    public class DonorService
    {
        private readonly DonorRepository donorRepository = new DonorRepository();
        private readonly LogicToDataMapperDonor logicToDataMapper = new LogicToDataMapperDonor();
        private readonly LogicToDataMapper logicToDataMapper2 = new LogicToDataMapper();
        private readonly DataToLogicMapperDonor dataToLogicMapper = new DataToLogicMapperDonor();


        public void AddDonorAccount(NewUserTransferObject nuto)
        {
            Donor newDonor = logicToDataMapper2.MapNewDonor(nuto);
            donorRepository.Save(newDonor);
        }

        public bool exists(string id)
        {
            return donorRepository.exists(id);
        }



        public List<DonationDetails> GetDonationDetails()
        {
            List<Data.Models.Donation> myDonations = donorRepository.GetDonations();
            List<DonationDetails> donationDetails = new List<DonationDetails>();
            foreach (Data.Models.Donation donation in myDonations)
            {
                donationDetails.Add(dataToLogicMapper.MapDonationToDonationDetails(donation));
            }

            return donationDetails;
        }

        public List<DonorDetailsTransferObject> GetDonorList()
        {
            List<DonorDetailsTransferObject> donorsTransferObject = new List<DonorDetailsTransferObject>();
            List<Donor> donors = donorRepository.GetDonors();
            foreach (Donor donor in donors)
            {
                donorsTransferObject.Add(dataToLogicMapper.MapDonorDetailsTransferObject(donor));
            }

            return donorsTransferObject;
        }


        public DonorDetailsTransferObject GetOne(string id)
        {
            return dataToLogicMapper.MapDonorDetailsTransferObject(donorRepository.GetOne(id));
        }



        public void EditDonorDetails(DonorDetailsTransferObject detailsTransferObject)
        {
            donorRepository.Save(logicToDataMapper.MapDonor(detailsTransferObject));
        }


    }
}