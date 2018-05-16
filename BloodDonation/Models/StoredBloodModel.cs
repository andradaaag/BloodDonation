using BloodDonation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class StoredBloodModel
    {
        public BloodType BloodType { get; set; }

        public DateTime CollectionDate { get; set; }

        //  will be chanced to an enum when passed forward
        public string Component { get; set; }
        

    }
}