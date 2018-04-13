using System;
using System.ComponentModel.DataAnnotations;

namespace BloodDonation.Models
{
    public class SignUpForm
    {
        public String UID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        public String Address { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public String OtherAddress { get; set; }
        public String OtherCity { get; set; }
        public String OtherCountry { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public int UserType { get; set; }

        public String Hospital { get; set; }
        public String DonationCenter { get; set; }
    }
}