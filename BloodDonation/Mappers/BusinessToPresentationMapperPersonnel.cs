
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
                Stage = donation.Stage.ToString(),
                Syphilis = donation.Syphilis,
                Thrombocytes = donation.Thrombocytes,
                DonationTime = donation.DonationTime
            };
        }
        public StoredBloodModel StoredBlood(Logic.Models.StoredBlood  b)
        {
            return new StoredBloodModel
            {
                BloodTypeGroup = b.BloodType.Group,
                BloodTypePH = b.BloodType.RH ? "Positive" : "Negative",
                Component = b.Component.ToString(),
                CollectionDate = b.CollectionDate,
                Amount = b.Amount,
                ID = b.ID
            };
        }
    }
}