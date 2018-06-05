using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class BookDonationDetails
    {
        public String donationHour { get; set; }
        public String donationDate { get; set; }
        public List<String> availableHours { get; set; }
        public String center { get; set; }
    }
}