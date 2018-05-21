
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
    /**
     * 
     */
    public class DonationCenterPersonnel : User
    {
        public bool isApproved { get; set; }
        public string DonationCenterID { get; set; }

        public DonationCenterPersonnel(): base("N/A", "N/A", "N/A", DateTime.Now, "N/A", "N/A", "N/A") {
            isApproved = false;
        }

        public DonationCenterPersonnel(string firstName, string lastName, string emailAddress, DateTime DOB, string address, string cityTown, string country)
            : base(firstName, lastName, emailAddress, DOB, address, cityTown, country) {
            isApproved = false;
        }

        override
        public string ToString()
        {
            return "Personnel(" + ID + ", " +firstName + ", " + lastName + ", " + emailAddress + ", " + DOB.ToShortDateString() 
                + ", " + Address + ", " + CityTown + ", " + Country + ", " + isApproved + ")";
        }

        public void validateAccount()
        {
            isApproved = true;
        }

        public bool isValidAccount()
        {
            return isApproved;
        }
    }
}