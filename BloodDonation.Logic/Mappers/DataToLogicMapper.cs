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

    }
}
