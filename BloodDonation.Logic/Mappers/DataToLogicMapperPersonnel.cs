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
            Donation don = new Donation
            {
                Alt = donation.Alt,
                BloodType = new BloodType
                {
                    Group = donation.BloodType.Group,
                    PH = donation.BloodType.PH
                },
                DonorId = donation.DonorId,
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
            return don;


        }
    }
}
