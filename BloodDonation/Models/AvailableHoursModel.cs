using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class AvailableHoursModel
    {
        public List<String> donationsCenterList { get; set; }
        public String donationCenter { get; set; }
        public String bookingDate { get; set; }
    }
}