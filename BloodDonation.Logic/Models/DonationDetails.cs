using System;
using BloodDonation.Data.Models;

namespace BloodDonation.Logic.Models
{
    public class DonationDetails
    {
        public String ID { get; set; }
        public String DonationDate { get; set; }
        public String CenterLocation { get; set; }
        public int Quantity { get; set; }
        public String TestResult { get; set; }
    }
}