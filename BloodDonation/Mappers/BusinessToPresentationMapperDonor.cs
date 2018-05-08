﻿using BloodDonation.Logic.Models;
using BloodDonation.Models;

namespace BloodDonation.Mappers
{
    public class BusinessToPresentationMapperDonor
    {
        public DonorDonationDetails MapDonorDonationDetails(DonationDetails donationDetails)
        {
            return new DonorDonationDetails()
            {
                ID = donationDetails.ID,
                CenterLocation = donationDetails.CenterLocation,
                Quantity = donationDetails.Quantity,
                TestResult = donationDetails.TestResult,
                DonationDate = donationDetails.DonationDate
            };
        }

        public DonorAccountRequest MapDonorAccountRequest(DonorDetailsTransferObject donorAccountDetails)
        {
            return new DonorAccountRequest()
            {
                ID = donorAccountDetails.ID,
                FirstName = donorAccountDetails.FirstName,
                LastName = donorAccountDetails.LastName,
                DateOfBirth = donorAccountDetails.DateOfBirth,
                Address = donorAccountDetails.Address,
                Weight = donorAccountDetails.Weight,
                Email = donorAccountDetails.Email,
                Country = donorAccountDetails.Country,
                Commentaries = donorAccountDetails.Commentaries
            };
        }
    }
}