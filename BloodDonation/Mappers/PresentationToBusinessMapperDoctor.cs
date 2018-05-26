﻿using BloodDonation.Logic.Models;
using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Mappers
{
    public class PresentationToBusinessMapperDoctor
    {

        public PatientDto MapPatientDtoViewToPatientDto(PatientDtoView patient)
        {
            return new PatientDto()
            {
                Id = patient.Id,
                Cnp = patient.Cnp,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                BloodQuantity = patient.BloodQuantity,
                PatientStatus = (Utils.Enums.PatientStatus)patient.PatientStatus
            };
        }
    }
}