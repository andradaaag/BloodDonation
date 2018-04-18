
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
/**
 * 
 */
	public class Doctor : User
	{


        public Doctor(string firstName, string lastName, string emailAddress, DateTime DOB, string address, string cityTown, string country) 
            : base(firstName, lastName, emailAddress, DOB, address, cityTown, country)
        {
        }

        public bool isApproved = false;

        /**
		 * 
		 */
        public HashSet<Hospital> Hospitals;

		/**
		 * 
		 */
		public HashSet<Request> Requests;

	}
}