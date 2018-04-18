
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
/**
 * 
 */
	public class Patient : BaseEntity
	{

		/**
		 * 
		 */
		public Patient()
		{
		}

		/**
		 * 
		 */
		public int noDonations;

		/**
		 * 
		 */
		public int bloodNeeded;

	}
}