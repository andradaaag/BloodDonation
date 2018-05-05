using BloodDonation.Logic.Models;
using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Mappers
{
    public class BusinessToPresentationMapper
    {
        public DoctorAccountRequest MapDoctorAccountRequest(AccountRequest accountRequest)
        {
            return new DoctorAccountRequest()
            {
                ID = accountRequest.ID,
                FirstName = accountRequest.FirstName,
                LastName = accountRequest.LastName,
                EmailAddress = accountRequest.EmailAddress,
                City = accountRequest.City,
                Country = accountRequest.Country,
                HospitalName = accountRequest.InstituteName,
                RequestType = accountRequest.RequestType

            };
        }

        public DonationCenterPersonnelAccountRequest MapDonationCenterPersonnelAccountRequest(AccountRequest accountRequest)
        {
            return new DonationCenterPersonnelAccountRequest()
            {
                ID = accountRequest.ID,
                FirstName = accountRequest.FirstName,
                LastName = accountRequest.LastName,
                EmailAddress = accountRequest.EmailAddress,
                City = accountRequest.City,
                Country = accountRequest.Country,
                HospitalName = accountRequest.InstituteName,
                RequestType = accountRequest.RequestType

            };
        }

        public DoctorDisplayData MapDoctorDisplayData(DoctorTransferObject doctor)
        {
            return new DoctorDisplayData()
            {
                ID = doctor.ID,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                EmailAddress = doctor.EmailAddress,
                City = doctor.City,
                Country = doctor.Country,
                HospitalName = doctor.InstituteName,
            };
        }

        public DonationCenterPersonnelDisplayData MapDonationCenterPersonnelDisplayData(DonationCenterPersonnelTransferObject dcpto)
        {
            return new DonationCenterPersonnelDisplayData()
            {
                ID = dcpto.ID,
                FirstName = dcpto.FirstName,
                LastName = dcpto.LastName,
                EmailAddress = dcpto.EmailAddress,
                City = dcpto.City,
                Country = dcpto.Country,
                CenterName = dcpto.InstituteName
            };

        }

        public HospitalDisplayData MapHospitalDisplayData(HospitalTransferObject hto)
        {
            return new HospitalDisplayData()
            {
                ID = hto.ID,
                Location = hto.Location,
                Name = hto.Name
            };
        }

        public DonationCenterDisplayData MapDonationCenterDisplayData(DonationCenterTransferObject dcto)
        {
            return new DonationCenterDisplayData()
            {
                ID = dcto.ID,
                Location = dcto.Location,
                Name = dcto.Name
            };
        }

        public DonorDonationDetails MapDonorDonationDetails(DonationDetails donationDetails)
        {
         return   new DonorDonationDetails()
         {
             ID = donationDetails.ID,
             CenterLocation = donationDetails.CenterLocation,
             Quantity = donationDetails.Quantity,
             TestResult = donationDetails.TestResult,
             DonationDate = donationDetails.DonationDate
         };
        }
    }
}