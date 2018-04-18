
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
/**
 * 
 */
	public class DonationCenterPersonnel : Donor
	{

		/**
		 * 
		 */
		public DonationCenterPersonnel()
		{
		}

		/**
		 * 
		 */
		public HashSet<DonationCenter> DonationCenter;



	}
}