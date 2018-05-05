using BloodDonation.Data.Models;
using BloodDonation.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Mappers
{
    public class DataToLogicMapper
    {
        public AccountRequest MapDoctorToAccountRequest(Doctor doctor)
        {
            return new AccountRequest()
            {
                ID = doctor.ID,
                FirstName = doctor.firstName,
                LastName = doctor.lastName,
                EmailAddress = doctor.emailAddress,
                City = doctor.CityTown,
                Country = doctor.Country,
                InstituteName = "unknown",
                RequestType = "Doctor"
            };
        }

        public AccountRequest MapDonationCenterPersonnelToAccountRequest(DonationCenterPersonnel doctor)
        {
            return new AccountRequest()
            {
                ID = doctor.ID,
                FirstName = doctor.firstName,
                LastName = doctor.lastName,
                EmailAddress = doctor.emailAddress,
                City = doctor.CityTown,
                Country = doctor.Country,
                InstituteName = "unknown",
                RequestType = "Personnel"
            };
        }

        public DoctorTransferObject MapDoctorTransferObject(Doctor doctor)
        {
            return new DoctorTransferObject()
            {
                ID = doctor.ID,
                FirstName = doctor.firstName,
                LastName = doctor.lastName,
                EmailAddress = doctor.emailAddress,
                City = doctor.CityTown,
                Country = doctor.Country,
                InstituteName = "unknown"
            };
        }

        public DonationCenterPersonnelTransferObject MapDonationCenterPersonnelTransferObject(DonationCenterPersonnel dcp)
        {
            return new DonationCenterPersonnelTransferObject()
            {
                ID = dcp.ID,
                FirstName = dcp.firstName,
                LastName = dcp.lastName,
                EmailAddress = dcp.emailAddress,
                City = dcp.CityTown,
                Country = dcp.Country,
                InstituteName = "unknown"
            };
        }

        public HospitalTransferObject MapHospitalTransferObject(Hospital hospital)
        {
            return new HospitalTransferObject()
            {
                ID = hospital.ID,
                Name = hospital.name,
                Location = hospital.location
            };
        }

        public DonationCenterTransferObject MapDonationCenterTransferObject(DonationCenter dc)
        {
            return new DonationCenterTransferObject()
            {
                ID = dc.ID,
                Name = dc.name,
                Location = dc.location
            };
        }

        public DonationDetails MapDonationToDonationDetails(Donation donation)
        {
            return new DonationDetails()
            {
                ID = donation.ID,
                CenterLocation = donation.center.location,
                Quantity = donation.quantity,
                TestResult = donation.testResult,
                DonationDate = donation.donationDate
            };
        }
    }
}