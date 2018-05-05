
using BloodDonation.Business.Mappers;
using BloodDonation.Data.Models;
using BloodDonation.Data.Repositories;
using BloodDonation.Logic.Mappers;
using BloodDonation.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Services
{
    public class HospitalService
    {
        private readonly HospitalRepository hospitalRepository = new HospitalRepository();
        private readonly LogicToDataMapper logicToDataMapper = new LogicToDataMapper();
        private readonly DataToLogicMapper dataToLogicMapper = new DataToLogicMapper();

        public List<HospitalTransferObject> GetAllHospitals()
        {
            List<Hospital> hospitals = hospitalRepository.findAll();
            List<HospitalTransferObject> hospitalTransferObjects = new List<HospitalTransferObject>();

            foreach(Hospital hospital in hospitals)
            {
                hospitalTransferObjects.Add(dataToLogicMapper.MapHospitalTransferObject(hospital));
            }

            return hospitalTransferObjects;
        }

        public void AddNewHospital(HospitalTransferObject newHospital)
        {
            hospitalRepository.save(logicToDataMapper.MapHospital(newHospital));
        }

        public List<HospitalTransferObject> FilterHospitalsByNameAndLocation(string nameQuery, string locationQuery)
        {
            List<Hospital> hospitals = hospitalRepository.findAll();
            List<HospitalTransferObject> hospitalTransferObjects = new List<HospitalTransferObject>();

            foreach(Hospital hospital in hospitals)
            {
                if(nameQuery != null && locationQuery != null)
                {
                    if (hospital.name.Contains(nameQuery) && hospital.location.Contains(locationQuery))
                        hospitalTransferObjects.Add(dataToLogicMapper.MapHospitalTransferObject(hospital));
                } else if (nameQuery != null)
                {
                    if(hospital.name.Contains(nameQuery))
                        hospitalTransferObjects.Add(dataToLogicMapper.MapHospitalTransferObject(hospital));
                } else
                {
                    if(hospital.location.Contains(locationQuery))
                        hospitalTransferObjects.Add(dataToLogicMapper.MapHospitalTransferObject(hospital));
                }
            }

            return hospitalTransferObjects;

        }
    }
}
