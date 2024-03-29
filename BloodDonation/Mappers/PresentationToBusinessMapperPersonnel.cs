﻿using BloodDonation.Models;
using BloodDonation.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Mappers
{
    public class PresentationToBusinessMapperPersonnel
    {

        public Logic.Models.Donation Donation(DonationModel donation)
        {
            return new Logic.Models.Donation
            {
                ID = donation.ID,
                Alt = donation.Alt,
                BloodType = new Logic.Models.BloodType
                {
                    Group = donation.BloodTypeGroup,
                    RH = donation.BloodTypeRH == "Positive"
                },
                DonorCNP = donation.DonorCNP,
                PatientCnp = donation.Cnp,
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
        public Logic.Models.StoredBlood StoredBlood(StoredBloodModel b)
        {
            return new Logic.Models.StoredBlood
            {
                BloodType = new Logic.Models.BloodType
                {
                    Group = b.BloodTypeGroup,
                    RH = b.BloodTypeRH == "Positive"
                },
                Component = (Component)Enum.Parse(typeof(Component), b.Component),
                CollectionDate = (b.CollectionDate - new DateTime(1970, 1, 1)).Seconds,
                Amount = b.Amount,
                ID = b.ID,
                DonationCenterID = b.DonationCenterID,
                DonorEmail = b.DonnorEmail
            };
        }

        public Logic.Models.Booking Booking(BookingModel b)
        {
            return new Logic.Models.Booking
            {
                ID = b.ID,
                Date = b.Date,
                DonationCenterId = b.DonationCenterId,
                DonorId = b.DonorId,
                DonorName = b.DonorName,
                Hour = b.Hour,
                UnixTime = (b.FullDate - new DateTime(1970, 1, 1)).Seconds
            };
        }

        public Logic.Models.DonationCenterPersonnel Personnel(Personnel p)
        {
            return new Logic.Models.DonationCenterPersonnel
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

        

        public Logic.Models.RequestPersonnel Request(RequestPersonnel r)
        {

            return new Logic.Models.RequestPersonnel
            {
                ID = r.ID,
                status = r.status,

                destination = r.destination,
                source = r.source,
                doctorId = r.doctorId,
                patientCnp = r.patientCnp,

                quantity = r.quantity,
                
                bloodType = new Logic.Models.BloodType
                {
                    Group = r.bloodType.Group,
                    RH = r.bloodType.PH,
                    bloodComponent = r.bloodType.component
                }
            };
        }

        public Logic.Models.SeparateBlood SeparateBlood(SeparateStoredBloodModel blood)
        {
            return new Logic.Models.SeparateBlood
            {
                ID = blood.ID,
                BloodType = new Logic.Models.BloodType
                {
                    Group = blood.BloodTypeGroup,
                    RH = blood.BloodTypeRH == "Positive"
                },
                CollectionDate = blood.CollectionDate,
                DonationCenterID = blood.DonationCenterID,
                RBC = blood.RBC,
                Plasma = blood.Plasma,
                Thrombocytes = blood.Thrombocytes,
                DonorEmail=blood.DonorEmail
            };
        }

        public Logic.Models.BloodType BloodType(BloodType bt)
        {
            return new Logic.Models.BloodType
            {
                Group = bt.Group,
                RH = bt.PH
            };
        }



    }

}