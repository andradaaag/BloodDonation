
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
		public bool PH;
		public void isCompatible(BloodType blood)
		{
			// TODO implement here
		}

	}
}