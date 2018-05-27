using BloodDonation.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Mappers
{
    class LogicToDataMapperPersonnel
    {
        public Data.Models.Donation Donation(Donation donation)
        {
            return new Data.Models.Donation
            {
                ID = donation.ID,
                Alt = donation.Alt,
                BloodType = new Data.Models.BloodType
                {
                    Group = donation.BloodType.Group,
                    RH = donation.BloodType.RH
                },
                DonorCnp = donation.DonorId,
                DonationCenterID = donation.DonationCenterId,
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

        internal Data.Models.StoredBlood StoredBlood(StoredBlood storedBlood)
        {
            return new Data.Models.StoredBlood
            {
                ID = storedBlood.ID,
                BloodType = new Data.Models.BloodType
                {
                    Group = storedBlood.BloodType.Group,
                    RH = storedBlood.BloodType.RH
                },
                Component = storedBlood.Component,
                CollectionDate = storedBlood.CollectionDate,
                Amount = storedBlood.Amount,
                DonationCenterID = storedBlood.DonationCenterID
            };
        }

        public Data.Models.DonationCenterPersonnel Personnel(DonationCenterPersonnel p)
        {
            return new Data.Models.DonationCenterPersonnel
            {
                ID = p.ID,
                isReviewed = p.isApproved,
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

        public Data.Models.Status Status(Status s)
        {
            return (Data.Models.Status)s;
        }
    }
}
