
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
/**
 * 
 */
	public class Doctor : Donor
	{

		/**
		 * 
		 */
		public Doctor()
		{
		}

		/**
		 * 
		 */
		public HashSet<Hospital> Doctors;

		/**
		 * 
		 */
		public HashSet<Request> Requests;

	}
}