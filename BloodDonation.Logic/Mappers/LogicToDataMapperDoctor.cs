using BloodDonation.Data.Models;
using BloodDonation.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Mappers
{
    class LogicToDataMapperDoctor
    {
        public Patient MapPatientDtoToPatient(PatientDto patientdto)
        {
            return new Patient()
            {
                ID = patientdto.Id,
                Cnp = patientdto.Cnp,
                FirstName = patientdto.FirstName,
                LastName = patientdto.LastName,
                BloodQuantity = patientdto.BloodQuantity,
                PatientStatus = patientdto.PatientStatus  
            };
        }

    }
}
