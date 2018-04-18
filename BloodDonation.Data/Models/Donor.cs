
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
/**
 * 
 */
	public class Donor
	{

		/**
		 * 
		 */
		public Donor()
		{
		}

		/**
		 * 
		 */
		public bool canDonate;

		/**
		 * 
		 */
		public DateTime lastDonation;

		/**
		 * 
		 */
		public string UID;

		/**
		 * 
		 */
		public string firstName;

		/**
		 * 
		 */
		public string lastName;

		/**
		 * 
		 */
		public DateTime DOB;

		/**
		 * 
		 */
		public string Address;

		/**
		 * 
		 */
		public string CityTown;

		/**
		 * 
		 */
		public string Country;

		/**
		 * 
		 */
		public string Residence;

		/**
		 * 
		 */
		public string ResCityTown;

		/**
		 * 
		 */
		public string ResCountry;

		/**
		 * 
		 */
		public DonationFormEntity DonationForm;

	}
}