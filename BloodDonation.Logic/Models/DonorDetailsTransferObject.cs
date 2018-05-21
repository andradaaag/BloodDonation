
using System;

namespace BloodDonation.Logic.Models
{
    public class DonorDetailsTransferObject
    {
        public String ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Address { get; set; }
        public int Weight { get; set; }
        public String Email { get; set; }
        public String Country { get; set; }
        public String Commentaries { get; set; }
        
    }
}