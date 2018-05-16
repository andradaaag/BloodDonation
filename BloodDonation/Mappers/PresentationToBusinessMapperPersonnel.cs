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
                bloodType = new Logic.Models.BloodType
                {
                    group = donation.BloodType.Group,
                    ph = donation.BloodType.PH == "Positive"
                },
                donorId = donation.DonorId,
                HepatitisB = donation.HepatitisB,
                HepatitisC = donation.HepatitisC,
                Hiv = donation.Hiv,
                Htlv = donation.Htlv,
                plasma = donation.Plasma,
                quantity = donation.Quantity,
                RBC = donation.RBC,
                stage = (Data.Models.Stage)Enum.Parse(typeof(Data.Models.Stage), donation.Stage),
                Syphilis = donation.Syphilis,
                Thrombocytes = donation.Thrombocytes
            };
            return don;


        }
    }
}