
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
/**
 * 
 */
	public class Donation : BaseEntity
	{

		/**
		 * 
		 */
		public Donation()
		{
		}

		/**
		 * 
		 */
		public BloodType bloodType;

		/**
		 * 
		 */
		public Donor donor;

		/**
		 * 
		 */
		public Stage stage;

		public String testResult;
		

		public DonationCenter center;

		public String donationDate;

		/**
		 * 
		 */
		public int quantity;

		public Donation(BloodType bloodType, Donor donor, Stage stage, int quantity, String testResult)
		{
			this.bloodType = bloodType;
			this.donor = donor;
			this.stage = stage;
			this.quantity = quantity;
			this.testResult = testResult;
		}
	}
}