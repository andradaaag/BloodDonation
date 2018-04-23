
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
		public Hospital(string location, string name)
		{
            this.location = location;
            this.name = name;
		}

        /**
		 * 
		 */
        public string location { get; set; }

		/**
		 * 
		 */
		public string name {get;set;}


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