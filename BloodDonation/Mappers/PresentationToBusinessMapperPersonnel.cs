using BloodDonation.Models;
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
                DonorId = donation.DonorId,
                HepatitisB = donation.HepatitisB,
                HepatitisC = donation.HepatitisC,
                Hiv = donation.Hiv,
                Htlv = donation.Htlv,
                Plasma = donation.Plasma,
                Quantity = donation.Quantity,
                RBC = donation.RBC,
                Stage = (Data.Models.Stage)Enum.Parse(typeof(Data.Models.Stage), donation.Stage),
                Syphilis = donation.Syphilis,
                Thrombocytes = donation.Thrombocytes,
                DonationTime = donation.DonationTime
            };
        }
        public Logic.Models.StoredBlood StoredBlood(StoredBloodModel b)
        {
            return new Logic.Models.StoredBlood
            {
                BloodType = new Logic.Models.BloodType
                {
                    Group = b.BloodTypeGroup,
                    RH = b.BloodTypePH == "Positive"
                },
                Component = (Data.Models.Component)Enum.Parse(typeof(Data.Models.Component), b.Component),
                CollectionDate = b.CollectionDate,
                Amount = b.Amount,
                ID = b.ID
            };
        }
    }
}