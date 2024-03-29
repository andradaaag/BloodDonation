﻿using System;
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
        private readonly DonationRepository donationRepository = new DonationRepository();
        private readonly LogicToDataMapper logicToDataMapper = new LogicToDataMapper();
        private readonly DataToLogicMapper dataToLogicMapper = new DataToLogicMapper();

        public bool exists(string id)
        {
            return doctorRepository.exists(id);
        }

        public bool isValidAccount(string id)
        {
            Doctor doctor = doctorRepository.GetOne(id);
            return doctor.isValidAccount();
        }

        public Doctor findById(string id)
        {
            return doctorRepository.GetOne(id);
        }

        public void AddDoctorAccount(NewUserTransferObject nuto)
        {
            Doctor newDoctor = logicToDataMapper.MapNewDoctor(nuto);

            doctorRepository.Save(newDoctor);
        }

        public List<AccountRequest> GetDoctorAccountRequests()
        {
            List<Doctor> myDoctors = doctorRepository.findAll();
            List<AccountRequest> myDoctorAccountRequests = new List<AccountRequest>();

            foreach (Doctor doctor in myDoctors)
            {
                if (!doctor.wasReviewed())
                {
                    myDoctorAccountRequests.Add(dataToLogicMapper.MapDoctorToAccountRequest(doctor));
                }
            }
            return myDoctorAccountRequests;
        }

        public List<DoctorTransferObject> GetValidDoctors()
        {
            List<Doctor> myDoctors = doctorRepository.findAll();
            List<DoctorTransferObject> doctorTransferObjects = new List<DoctorTransferObject>();

            foreach (Doctor doctor in myDoctors)
            {
                if (doctor.isValidAccount())
                {
                    doctorTransferObjects.Add(dataToLogicMapper.MapDoctorTransferObject(doctor));
                }
            }
            return doctorTransferObjects;
        }

        public List<DoctorTransferObject> FilterDoctorsBySearchQuery(string searchQuery)
        {
            if (searchQuery.Length == 0)
                return GetValidDoctors();

            List<DoctorTransferObject> doctorTransferObjects = GetValidDoctors();
            List<DoctorTransferObject> respone = new List<DoctorTransferObject>();

            foreach (DoctorTransferObject dto in doctorTransferObjects)
            {
                if (dto.FirstName.Contains(searchQuery) || dto.LastName.Contains(searchQuery))
                    respone.Add(dto);
            }

            return respone;
        }

        public void RemoveCnp(String cnp)
        {
            donationRepository.FindByPatientCnp(cnp)
                                 .ForEach(el =>
                                 {
                                     el.PatientCnp = "";
                                     donationRepository.Edit(el);
                                 });

        }

        public void ApproveAccount(string id)
        {
            Doctor d = doctorRepository.GetOne(id);
            d.validateAccount();
            doctorRepository.Save(d);
        }

        public void DeleteAccount(string id)
        {
            Doctor d= doctorRepository.GetOne(id);
            d.invalidateAccount();
            doctorRepository.Save(d);
        }

    }
}
