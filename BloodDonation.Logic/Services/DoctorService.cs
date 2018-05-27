using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Data.Repositories;
using BloodDonation.Business.Mappers;
using BloodDonation.Logic.Models;
using BloodDonation.Logic.Mappers;
using BloodDonation.Data.Models;

namespace BloodDonation.Logic.Services
{
    public class DoctorService
    {
        private readonly DoctorRepository doctorRepository = new DoctorRepository();
        private readonly LogicToDataMapper logicToDataMapper = new LogicToDataMapper();
        private readonly DataToLogicMapper dataToLogicMapper = new DataToLogicMapper();

      

        public bool IsIDPresent(string id)
        {
            return doctorRepository.IsIDPresent(id);
        }

        public void AddDoctorAccount(NewUserTransferObject nuto)
        {
            doctorRepository.Save(logicToDataMapper.MapNewDoctor(nuto));
        }

        public List<AccountRequest> GetDoctorAccountRequests()
        {
            return doctorRepository.findAll()
                                   .AsEnumerable()
                                   .Select(el => dataToLogicMapper.MapDoctorToAccountRequest(el))
                                   .ToList();
        } 

        public List<DoctorTransferObject> GetValidDoctors()
        {

            return doctorRepository.findAll()
                                  .AsEnumerable()
                                  .Where(el => el.isValidAccount())
                                  .Select(el => dataToLogicMapper.MapDoctorTransferObject(el))
                                  .ToList();

        }

        public List<DoctorTransferObject> FilterDoctorsBySearchQuery(string searchQuery)
        {
            if (searchQuery.Length == 0)
                return GetValidDoctors();

            List<DoctorTransferObject> doctorTransferObjects = GetValidDoctors();
            List<DoctorTransferObject> respone = new List<DoctorTransferObject>();

            foreach(DoctorTransferObject dto in doctorTransferObjects)
            {
                if (dto.FirstName.Contains(searchQuery) || dto.LastName.Contains(searchQuery))
                    respone.Add(dto);
            }

            return respone;
        }
    }
}
