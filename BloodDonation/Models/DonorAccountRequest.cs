using System;
using System.ComponentModel.DataAnnotations;

namespace BloodDonation.Models
{
    public class DonorAccountRequest
    {
        public String ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Cnp { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public String Address { get; set; }
        public int Weight { get; set; }
        public String Email { get; set; }
        public String Country { get; set; }
        public String Commentaries { get; set; }
    }
}