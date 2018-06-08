using System;

namespace BloodDonation.Models
{
    public class DonorDonationDetails
    {
        public String ID { get; set; }
        public String DonationDate { get; set; }
        public String CenterLocation { get; set; }
        public int Quantity { get; set; }
        public bool TestResult { get; set; }
    }
}