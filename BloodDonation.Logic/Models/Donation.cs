﻿using BloodDonation.Data.Models;
using BloodDonation.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Models
{
    public class Donation
    {
        public Donation()
        {
        }

        public string ID { get; set; }
        public BloodType BloodType;

        public string DonorCNP;

        public string PatientCnp;
        
        public Stage Stage;

        public String donationHour { get; set; }
        public String donationDate { get; set; }

        public int Quantity;
        public int Plasma;
        public int RBC;
        public int Thrombocytes;

        public bool Hiv { get; set; }
        public bool HepatitisB { get; set; }
        public bool HepatitisC { get; set; }
        public bool Syphilis { get; set; }
        public bool Htlv { get; set; }
        public bool Alt { get; set; }

        public string DonationCenterId { get; set; }
        public long DonationTime { get; set; }
        public bool IsAccepted() => !(Hiv && HepatitisB && HepatitisC && Syphilis && Htlv && Alt);

    }
}
