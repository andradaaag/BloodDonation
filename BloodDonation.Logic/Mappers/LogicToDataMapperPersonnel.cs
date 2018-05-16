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
                bloodType = new Data.Models.BloodType
                {
                    group = donation.bloodType.group,
                    ph = donation.bloodType.ph
                },
                donorId = donation.donorId,
                HepatitisB = donation.HepatitisB,
                HepatitisC = donation.HepatitisC,
                Hiv = donation.Hiv,
                Htlv = donation.Htlv,
                plasma = donation.plasma,
                quantity = donation.quantity,
                RBC = donation.RBC,
                stage = donation.stage,
                Syphilis = donation.Syphilis,
                Thrombocytes = donation.Thrombocytes
            };
            return don;


        }
    }
}
