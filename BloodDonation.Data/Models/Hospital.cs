
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
/**
 * 
 */
	public class Hospital : BaseEntity
	{

		/**
		 * 
		 */
		public Hospital()
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
		public HashSet<Doctor> Doctors;

		/**
		 * 
		 */
		public HashSet<Patient> Patients;

	}
}