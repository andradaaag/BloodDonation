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
            Logic.Models.Donation don = new Logic.Models.Donation
            {
                Alt = donation.Alt,
                BloodType = new Logic.Models.BloodType
                {
                    Group = donation.BloodType.Group,
                    PH = donation.BloodType.PH == "Positive"
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
            return don;


        }
    }
}