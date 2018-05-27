using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
/**
 * 
 */
	public class Donor : User
	{
		/**
		 * 
		 */
		public Donor()
		{
		}

		/**
		 * 
		 */
		public bool canDonate;

		/**
		 * 
		 */
		public DateTime lastDonation;

		public String additionalCommentaries;

        public String cnp;
		/**
		 * 
		 */
		public DonationFormEntity DonationFormEntity;

		public Donor(string firstName, string lastName, string emailAddress, DateTime dOB, string address,
			string cityTown, string country, DonationFormEntity donationFormEntity, String comms, String cnp) :
			base(firstName, lastName, emailAddress, dOB, address, cityTown, country)
		{
			DonationFormEntity = donationFormEntity;
			additionalCommentaries = comms;
            this.cnp = cnp;
		}
	}
}