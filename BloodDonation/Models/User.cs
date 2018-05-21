using System;
using System.ComponentModel.DataAnnotations;

namespace BloodDonation.Models
{
    public class User : BaseEntity
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string CityTown { get; set; }
        public string Country { get; set; }
        public string Residence { get; set; }
        public string ResCityTown { get; set; }
        public string ResCountry { get; set; }

        public User() { }

        public User(string firstName, string lastName, string emailAddress, DateTime dOB, string address, string cityTown, string country)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.emailAddress = emailAddress;
            DOB = dOB;
            Address = address;
            CityTown = cityTown;
            Country = country;
        }
    }
}