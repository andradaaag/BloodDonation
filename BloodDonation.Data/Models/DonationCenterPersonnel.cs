
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

        public DonationCenterPersonnel(string firstName, string lastName, string emailAddress, DateTime DOB, string address, string cityTown, string country)
                   : base(firstName, lastName, emailAddress, DOB, address, cityTown, country)
        {
        }

        private bool isApproved = false;

        public bool isValidAccount()
        {
            return this.isApproved;
        }

        public void validateAccount()
        {
            this.isApproved = true;
        }

        public void invalidateAccount()
        {
            this.isApproved = false;
        }

        public HashSet<DonationCenter> DonationCenter;

	}
}