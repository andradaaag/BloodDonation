using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Data.Repositories;
using BloodDonation.Business.Mappers;
using BloodDonation.Business.Models;
using BloodDonation.Logic.Mappers;
using BloodDonation.Data.Models;
using BloodDonation.Logic.Models;

namespace BloodDonation.Logic.Services
{
    public class DoctorService
    {
        private readonly DoctorRepository doctorRepository = new DoctorRepository();
        private readonly LogicToDataMapper logicToDataMapper = new LogicToDataMapper();
        private readonly DataToLogicMapper dataToLogicMapper = new DataToLogicMapper();

        public async Task<List<AccountRequest>> GetDoctorAccountRequests()
        {
            List<Doctor> myDoctors =  await doctorRepository.FindAll();
            List<AccountRequest> myDoctorAccountRequests = new List<AccountRequest>();
            
            foreach(Doctor doctor in myDoctors){
                if (!doctor.isValidAccount())
                {
                    myDoctorAccountRequests.Add(dataToLogicMapper.MapDoctorToAccountRequest(doctor));
                }
            }
            return myDoctorAccountRequests;
        } 

        public async Task<List<DoctorTransferObject>> GetValidDoctors()
        {
            List<Doctor> myDoctors = await doctorRepository.FindAll();
            List<DoctorTransferObject> doctorTransferObjects = new List<DoctorTransferObject>();

            foreach(Doctor doctor in myDoctors)
            {
                if (doctor.isValidAccount())
                {
                    doctorTransferObjects.Add(dataToLogicMapper.MapDoctorTransferObject(doctor));
                }
            }
            return doctorTransferObjects;
        }

        public async Task<List<DoctorTransferObject>> FilterDoctorsBySearchQuery(string searchQuery)
        {
            if (searchQuery.Length == 0)
                return await GetValidDoctors();

            List<DoctorTransferObject> doctorTransferObjects = await GetValidDoctors();
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
