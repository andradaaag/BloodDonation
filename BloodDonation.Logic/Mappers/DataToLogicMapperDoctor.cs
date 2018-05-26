using BloodDonation.Data.Models;
using BloodDonation.Logic.Models;
using BloodDonation.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Mappers
{
    class DataToLogicMapperDoctor
    {

        public PatientDto MapPatientToPatientDto(Patient patient)
        {
            return new PatientDto()
            {
               Id = patient.ID,
               Cnp = patient.Cnp,
               FirstName = patient.FirstName,
               LastName = patient.LastName,
               BloodQuantity = patient.BloodQuantity,
               PatientStatus = (PatientStatus)patient.PatientStatus
            };
        }

    }
}
