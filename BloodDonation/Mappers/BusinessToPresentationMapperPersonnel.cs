
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
            DonationModel don = new DonationModel
            {
                Alt = donation.Alt,
                BloodType = new BloodType
                {
                    Group = donation.bloodType.group,
                    PH = donation.bloodType.ph ? "Positive" : "Negative"
                },
                DonorId = donation.donorId,
                HepatitisB = donation.HepatitisB,
                HepatitisC = donation.HepatitisC,
                Hiv = donation.Hiv,
                Htlv = donation.Htlv,
                Plasma = donation.plasma,
                Quantity = donation.quantity,
                RBC = donation.RBC,
                Stage = donation.stage.ToString(),
                Syphilis = donation.Syphilis,
                Thrombocytes = donation.Thrombocytes
            };
            return don;
        }
    }
}