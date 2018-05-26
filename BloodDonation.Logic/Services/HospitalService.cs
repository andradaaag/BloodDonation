
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
            return hospitalRepository.FindAll()
                                     .AsEnumerable()
                                     .Select(el => dataToLogicMapper.MapHospitalTransferObject(el))
                                     .ToList();
        }

        public void AddNewHospital(HospitalTransferObject newHospital)
        {
            hospitalRepository.Save(logicToDataMapper.MapHospital(newHospital));
        }

        public List<HospitalTransferObject> FilterHospitalsByNameAndLocation(String nameQuery, String locationQuery)
        {
            return hospitalRepository.FindByName(nameQuery)
                                     .AsEnumerable()
                                     .Where(el => el.location == locationQuery)
                                     .Select(el => dataToLogicMapper.MapHospitalTransferObject(el))
                                     .ToList();
        }
    }
}
