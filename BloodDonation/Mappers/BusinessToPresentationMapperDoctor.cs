﻿using BloodDonation.Data.Models;
using BloodDonation.Logic.Models;
using BloodDonation.Logic.Services;
using BloodDonation.Models;
using BloodDonation.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Mappers
{
    public class BusinessToPresentationMapperDoctor
    {
        private DoctorService doctorService = new DoctorService();

        
        public Logic.Models.RequestPersonnel MapRequestBloodFormToRequestPersonnel(RequestBloodForm request, String uid)
        {
            Logic.Models.RequestPersonnel newRequest = new Logic.Models.RequestPersonnel();
            newRequest.bloodType.Group = request.bloodGroup;
            newRequest.bloodType.RH = request.bloodRh == "positive" ? true : false;
            newRequest.bloodType.bloodComponent = request.bloodComponent;
            newRequest.patientCnp = request.patientCnp;
            newRequest.quantity = request.quantity;
            newRequest.status = Status.BeingProcessed;
            newRequest.doctorId = uid;
            newRequest.urgency = request.urgency;
            Doctor doctor = doctorService.findById(newRequest.doctorId);
            if (doctor == null)
            {
                return null;
            }
            newRequest.destination = doctor.HospitalId;

            newRequest.source = "none";
            return newRequest;
        }

    }
}