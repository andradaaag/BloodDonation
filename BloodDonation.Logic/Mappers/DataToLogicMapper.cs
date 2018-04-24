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

        public AccountRequest MapDCPersonnelToAccountRequest(DonationCenterPersonnel personnel)
        {
            return new AccountRequest()
            {
                ID = personnel.ID,
                FirstName = personnel.firstName,
                LastName = personnel.lastName,
                EmailAddress = personnel.emailAddress,
                City = personnel.CityTown,
                Country = personnel.Country,
                InstituteName = "unknown",
                RequestType = "DonationCenterPersonnel"
            };
        }

    }
}
