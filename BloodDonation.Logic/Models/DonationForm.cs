using System;

namespace BloodDonation.Logic.Models
{
    public class DonationForm
    {
        public String UID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime DOB { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public String OtherAddress { get; set; }
        public String OtherCity { get; set; }
        public String OtherCountry { get; set; }
    }
}
