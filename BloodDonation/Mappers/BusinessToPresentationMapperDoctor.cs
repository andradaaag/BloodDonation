using BloodDonation.Data.Models;
using BloodDonation.Logic.Models;
using BloodDonation.Logic.Services;
using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Mappers
{
    public class BusinessToPresentationMapperDoctor
    {
        private DoctorService doctorService = new DoctorService();

        public PatientDtoView MapPatientDtoToPatientDtoView(PatientDto patient)
        {
            return new PatientDtoView()
            {
                Id = patient.Id,
                Cnp = patient.Cnp,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                BloodQuantity = patient.BloodQuantity,
                PatientStatus = (Utils.Enums.PatientStatus)patient.PatientStatus
            };
        }

        public Logic.Models.RequestPersonnel MapRequestBloodFormToRequestPersonnel(RequestBloodForm request, String uid)
        {
            Logic.Models.RequestPersonnel newRequest = new Logic.Models.RequestPersonnel();
            newRequest.bloodType.Group = request.bloodGroup;
            newRequest.bloodType.RH = request.bloodRh == "positive" ? true : false;
            newRequest.bloodType.bloodComponent = request.bloodComponent;
            newRequest.patientCnp = request.patientCnp;
            newRequest.quantity = request.quantity;
            newRequest.status = Logic.Models.Status.BeingProcessed;
            newRequest.doctorId = uid;
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