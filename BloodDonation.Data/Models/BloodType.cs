
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

		public BloodType() {}
		public string Group;

		/**
		 * 
		 */
		public bool RH;

		/**
		 * @param blood
		 */

		public void isCompatible(BloodType blood)
		{
			// TODO implement here
		}

		public BloodType(string group)
		{
			this.Group = group;
		}
	}
}