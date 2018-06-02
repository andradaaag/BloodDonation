
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class SeparateStoredBloodModel
    {
        public string ID { get; set; }

        public string DonorEmail { get; set; }
        public string BloodTypeGroup { get; set; }
        public string BloodTypeRH { get; set; }

        public int RBC { get; set; }
        public int Plasma { get; set; }
        public int Thrombocytes { get; set; }

        public long CollectionDate { get; set; }

        public string DonationCenterID { get; set; }
    }
}