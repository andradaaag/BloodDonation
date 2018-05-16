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
            Data.Models.Donation don = new Data.Models.Donation
            {
                Alt = donation.Alt,
                BloodType = new Data.Models.BloodType
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
