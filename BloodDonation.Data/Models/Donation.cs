
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

		/**
		 * 
		 */
		public int quantity;


	}
}