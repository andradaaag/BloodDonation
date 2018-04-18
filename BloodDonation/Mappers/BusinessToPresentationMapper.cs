﻿using BloodDonation.Logic.Models;
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

    }
}