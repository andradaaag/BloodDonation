﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class StoredBloodModel
    {
        public string ID { get; set; }

        public string BloodTypeGroup { get; set; }
        public string BloodTypeRH { get; set; }

        public int Amount { get; set; }

        public DateTime CollectionDate { get; set; }
        //  will be chanced to an enum when passed forward
        public string Component { get; set; }
        public string DonationCenterID { get; set; }
        public string DonnorEmail { get; set; }

    }
}