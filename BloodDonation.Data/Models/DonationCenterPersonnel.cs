
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

		/**
		 * 
		 */
		public DonationCenterPersonnel()
		{
		}


        public bool isApproved = false;
        /**
		 * 
		 */
        public HashSet<DonationCenter> DonationCenter;

	}
}