
using BloodDonation.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Mappers
{
    class DataToLogicMapperPersonnel
    {
        public Donation Donation(Data.Models.Donation donation)
        {
            return new Donation
            {
                ID = donation.ID,
                Alt = donation.Alt,
                BloodType = new BloodType
                {
                    Group = donation.BloodType.Group,
                    RH = donation.BloodType.RH
                },
                DonationCenterId = donation.DonationCenterID,
                PatientCnp = donation.PatientCnp,
                DonorCNP = donation.DonorCnp,
                HepatitisB = donation.HepatitisB,
                HepatitisC = donation.HepatitisC,
                Hiv = donation.Hiv,
                Htlv = donation.Htlv,
                Plasma = donation.Plasma,
                Quantity = donation.Quantity,
                RBC = donation.RBC,
                Stage = donation.Stage,
                Syphilis = donation.Syphilis,
                Thrombocytes = donation.Thrombocytes,
                DonationTime = donation.DonationTime
            };
            
        }

        internal StoredBlood StoredBlood(Data.Models.StoredBlood i)
        {
            return new StoredBlood
            {
                ID = i.ID,
                BloodType = new BloodType
                {
                    Group = i.BloodType.Group,
                    RH = i.BloodType.RH
                },
                Component = i.Component,
                CollectionDate = i.CollectionDate,
                Amount = i.Amount,
                DonationCenterID = i.DonationCenterID,
                DonorEmail = i.DonorEmail
                
            };
            
        }

        public DonationCenterPersonnel Personnel(Data.Models.DonationCenterPersonnel p)
        {
            return new DonationCenterPersonnel
            {
                ID = p.ID,
                isApproved = p.isReviewed,
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



        public BloodType convertBloodType(Data.Models.BloodType bt)
        {
            return new BloodType
            {
                Group = bt.Group,
                RH = bt.RH,
                bloodComponent = bt.Component
            };
        }

       
        public RequestPersonnel Request(Data.Models.Request r)
        {
            return new RequestPersonnel
            {
                ID = r.ID,
                urgency = r.urgency,
                patientCnp = r.patientCnp,
                status = r.status,
                destination = r.destination,
                source = r.source,
                doctorId = r.doctorId,
                quantity = r.quantity,
                bloodType = this.convertBloodType(r.bloodType)
            };

        }
        public Booking Booking(Data.Models.Booking b)
        {
            return new Booking
            {
                ID = b.ID,
                Date = b.Date,
                DonationCenterId = b.DonationCenterId,
                DonorId = b.DonorId,
                DonorName = b.DonorName,
                Hour = b.Hour,
                UnixTime = b.UnixTime
            };
        }
    }
}
