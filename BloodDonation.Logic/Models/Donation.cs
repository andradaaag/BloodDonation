using BloodDonation.Data.Models;
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

        public BloodType bloodType;

        public string donorId;

        public Stage stage;

        public int quantity;

        public int plasma;
        public int RBC;
        public int Thrombocytes;

        public bool Hiv { get; set; }
        public bool HepatitisB { get; set; }
        public bool HepatitisC { get; set; }
        public bool Syphilis { get; set; }
        public bool Htlv { get; set; }
        public bool Alt { get; set; }

    }
}
