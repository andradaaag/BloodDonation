
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
                DonorId = donation.DonorCnp,
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
                Amount = i.Amount
            };
            
        }

        public Personnel Personnel(Data.Models.Personnel p)
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
                ResCountry = p.ResCountry
            };
        }



        public BloodType BloodType(Data.Models.BloodType bt)
        {
            return new BloodType
            {
                Group = bt.Group,
                RH = bt.RH
            };
        }

        public Status Status(Data.Models.Status s)
        {
            return (Status)s;
        }

        public RequestPersonnel Request(Data.Models.Request r)
        {
            return new RequestPersonnel
            {
                ID = r.ID,
                status = this.Status(r.status),
                hospitalName = r.destination.name,
                hospitalLocation = r.destination.location,
                quantity = r.quantity,
                bloodType = this.BloodType(r.bloodType)
            };

        }
    }
}
