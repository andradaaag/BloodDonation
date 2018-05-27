﻿
using BloodDonation.Logic.Models;
using BloodDonation.Logic.Services;
using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Mappers
{
    public class BusinessToPresentationMapperPersonnel
    {
        public DonationModel Donation(Logic.Models.Donation donation)
        {
            return new DonationModel
            {
                ID = donation.ID,
                Alt = donation.Alt,
                BloodTypeGroup = donation.BloodType.Group,
                BloodTypeRH = donation.BloodType.RH ? "Positive" : "Negative",
                DonorId = donation.DonorId,
                HepatitisB = donation.HepatitisB,
                HepatitisC = donation.HepatitisC,
                Hiv = donation.Hiv,
                Htlv = donation.Htlv,
                Plasma = donation.Plasma,
                Quantity = donation.Quantity,
                RBC = donation.RBC,
                Syphilis = donation.Syphilis,
                Thrombocytes = donation.Thrombocytes,
                DonationTime = donation.DonationTime,
                DonationCenterId = donation.DonationCenterId
            };
        }
        public StoredBloodModel StoredBlood(Logic.Models.StoredBlood  b)
        {
            return new StoredBloodModel
            {
                BloodTypeGroup = b.BloodType.Group,
                BloodTypeRH = b.BloodType.RH ? "Positive" : "Negative",
                Component = b.Component.ToString(),
                CollectionDate = new DateTime(1970, 1, 1).AddSeconds(b.CollectionDate),
                Amount = b.Amount,
                ID = b.ID,
                DonationCenterID = b.DonationCenterID
            };
        }

        public Personnel Personnel(Logic.Models.DonationCenterPersonnel p)
        {
            return new Personnel
            {
                ID = p.ID,
                isApproved = p.isApproved,
                firstName = p.firstName,
                lastName = p.lastName,
                emailAddress = p.emailAddress,
                DOB = p.DOB,
                Address = p.Address,
                CityTown = p.CityTown,
                Country = p.Country,
                Residence = p.Residence,
                ResCityTown = p.ResCityTown,
                ResCountry = p.ResCountry,
                DonationCenterID = p.DonationCenterID
            };
        }
       

        public Models.RequestPersonnel Request(Logic.Models.RequestPersonnel r)
        {
            HospitalService hs = new HospitalService();
            HospitalTransferObject h = hs.GetHospitalById(r.destination);

            return new Models.RequestPersonnel
            {
                ID = r.ID,
                status = (Models.Status)r.status,

                destination = r.destination,
                source = r.source,
                doctorId = r.doctorId,
                patientCnp = r.patientCnp,

                quantity = r.quantity,
                bloodType = new Models.BloodType
                {
                    Group = r.bloodType.Group,
                    RH = r.bloodType.RH
                },

                hospitalName = h.Name,
                hospitalLocation = h.Location,       
                
            };
        }
        public SeparateStoredBloodModel SeparateBlood(Logic.Models.StoredBlood sb)
        {
            return new SeparateStoredBloodModel
            {
                ID = sb.ID,
                BloodTypeGroup = sb.BloodType.Group,
                BloodTypeRH = sb.BloodType.RH ? "Positive" : "Negative",
                CollectionDate = sb.CollectionDate,
                DonationCenterID = sb.DonationCenterID
            };
        }
    }
}