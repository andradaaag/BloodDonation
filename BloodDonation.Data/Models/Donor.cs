
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

		/**
		 * 
		 */
		public DonationFormEntity DonationFormEntity;

	}
}
