using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Models
{
    public class SeparateBlood
    {
        public string ID { get; set; }

        public BloodType BloodType { get; set; }
        
        public int RBC { get; set; }
        public int Plasma { get; set; }
        public int Thrombocytes { get; set; }

        public long CollectionDate { get; set; }
        //  will be chanced to an enum when passed forward
        public string DonationCenterID { get; set; }
    }
}
