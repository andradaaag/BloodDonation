
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

        public bool isApproved = false;
        /**
		 * 
		 */
        public HashSet<DonationCenter> DonationCenter;

	}
}