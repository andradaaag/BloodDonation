
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class SeparateStoredBloodModel
    {
        public string ID { get; set; }

        public string BloodTypeGroup { get; set; }
        public string BloodTypeRH { get; set; }

        public string RBC { get; set; }
        public string Plasma { get; set; }
        public string Thrombocytes { get; set; }

        public long CollectionDate { get; set; }
        //  will be chanced to an enum when passed forward
        public string Component { get; set; }
        public string DonationCenterID { get; set; }
    }
}