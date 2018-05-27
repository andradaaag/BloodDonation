
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
        public bool isReviewed { get; set; } 


        public bool isValid { get; set; }


        public bool wasReviewed()
        {
            return this.isReviewed;
        }


        public DonationCenterPersonnel(): base("N/A", "N/A", "N/A", DateTime.Now, "N/A", "N/A", "N/A") {
            isReviewed = false;
            isValid = false;
        }

        public DonationCenterPersonnel(string firstName, string lastName, string emailAddress, DateTime DOB, string address, string cityTown, string country)
            : base(firstName, lastName, emailAddress, DOB, address, cityTown, country) {
            isReviewed = false;
            isValid = false;
        }

        override
        public string ToString()
        {
            return "Personnel(" + ID + ", " +firstName + ", " + lastName + ", " + emailAddress + ", " + DOB.ToShortDateString() 
                + ", " + Address + ", " + CityTown + ", " + Country + ", " + isReviewed + ")";
        }

        public void validateAccount()
        {
            isReviewed = true;
            isValid = true;
        }

        public void invalidateAccount()
        {
            isReviewed = true;
            isValid = false;
        }

        public bool isValidAccount()
        {
            return isReviewed && isValid;
        }
    }
}