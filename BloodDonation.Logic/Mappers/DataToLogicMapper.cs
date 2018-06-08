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

        public AccountRequest MapDonationCenterPersonnelToAccountRequest(Data.Models.DonationCenterPersonnel doctor)
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
                RequestType = "DonationCenterPersonnel"
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

        public DonationCenterPersonnelTransferObject MapDonationCenterPersonnelTransferObject(Data.Models.DonationCenterPersonnel dcp)
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

        public HospitalTransferObject MapHospitalTransferObject(Data.Models.Hospital hospital)
        {
            return new HospitalTransferObject()
            {
                ID = hospital.ID,
                Name = hospital.name,
                Location = hospital.location,
                Lat = hospital.lat,
                Lon = hospital.lon
            };
        }

        public DonationCenterTransferObject MapDonationCenterTransferObject(Data.Models.DonationCenter dc)
        {
            return new DonationCenterTransferObject()
            {
                ID = dc.ID,
                Name = dc.name,
                Location = dc.location,
                Lat = dc.Lat,
                Lon = dc.Lon
            };
        }


    }
}