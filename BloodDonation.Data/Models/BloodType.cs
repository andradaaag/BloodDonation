
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
/**
 * 
 */
	public class BloodType : BaseEntity
	{

		/**
		 * 
		 */
		public BloodType()
		{
		}

		/**
		 * 
		 */
		public string group;

		/**
		 * 
		 */
		public bool ph;

		/**
		 * @param blood
		 */
		public void isCompatible(BloodType blood)
		{
			// TODO implement here
		}

	}
}