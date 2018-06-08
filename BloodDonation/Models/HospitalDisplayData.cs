using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class HospitalDisplayData
    {
        public String ID { get; set; }
        public String Location { get; set; }
        public String Name { get; set; }
        public Double Lat { get; set; }
        public Double Lon { get; set; }

    }
}