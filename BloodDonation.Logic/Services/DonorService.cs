﻿using System;
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
        private readonly LogicToDataMapper logicToDataMapper = new LogicToDataMapper();
        private readonly DataToLogicMapperDonor dataToLogicMapper = new DataToLogicMapperDonor();


        public void AddDonationForm(DonationForm form)
        {
            var firebaseDonationForm = logicToDataMapper.MapDonationForm(form);
            donorRepository.AddDonationForm(firebaseDonationForm);
        }

        public List<DonationDetails> GetDonationDetails()
        {
            List<Donation> myDonations = donorRepository.GetDonations();
            List<DonationDetails> donationDetails = new List<DonationDetails>();
            foreach (Donation donation in myDonations)
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

        public DonorDetailsTransferObject GetDonorDetailsById(String ID)
        {
            List<Donor> donors = donorRepository.GetDonors();
            foreach (Donor donor in donors)
            {
                if (donor.ID == ID)
                {
                    return dataToLogicMapper.MapDonorDetailsTransferObject(donor);
                }
            }
            return null;
        }

    }
}