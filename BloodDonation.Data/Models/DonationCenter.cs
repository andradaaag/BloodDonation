
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
/**
 * 
 */
	public class DonationCenter : BaseEntity
	{

		/**
		 * 
		 */
		public DonationCenter()
		{
		}

		/**
		 * 
		 */
		public string location;

		/**
		 * 
		 */
		public string name;

		/**
		 * 
		 */
		public HashSet<DonationCenterPersonnel> Personnel;

		/**
		 * 
		 */
		public HashSet<StoredBlood> StoredBlood;

	}
}