using BloodDonation.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Data.Repositories;
using BloodDonation.Logic.Mappers;

namespace BloodDonation.Logic.Services
{
    class PatientService
    {
        private readonly PatientRepository patientRepository = new PatientRepository();
        private readonly LogicToDataMapperDoctor logicToDataMapperDoctor = new LogicToDataMapperDoctor();
        private readonly DataToLogicMapperDoctor dataToLogicMapperDoctor = new DataToLogicMapperDoctor();

        public List<PatientDto> GetAllPatients()
        {
            return  patientRepository.FindAll()
                                     .AsEnumerable()
                                     .Select(el => dataToLogicMapperDoctor.MapPatientToPatientDto(el))
                                     .ToList();
        }

        public PatientDto GetPatientByCnp(String Cnp)
        {
            return dataToLogicMapperDoctor
                        .MapPatientToPatientDto(patientRepository.FindByCnp(Cnp));
        }

        public void AddPatient(PatientDto patientDto)
        {
            patientRepository
                .Save(logicToDataMapperDoctor.MapPatientDtoToPatient(patientDto));
        }



    }
}
