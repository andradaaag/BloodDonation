using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Models
{
    public class User : BaseEntity
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        public string Address { get; set; }
        public string CityTown { get; set; }
        public string Country { get; set; }
        public string Residence { get; set; }
        public string ResCityTown { get; set; }
        public string ResCountry { get; set; }

        public User(){}

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
