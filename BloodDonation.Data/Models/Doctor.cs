
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

        public Doctor() { }

        public Doctor(string firstName, string lastName, string emailAddress, DateTime DOB, string address, string cityTown, string country) 
            : base(firstName, lastName, emailAddress, DOB, address, cityTown, country)
        {
        }

        public Doctor(string firstName, string lastName, string emailAddress, DateTime DOB, string address, string cityTown, string country, string hospitalId)
            : base(firstName, lastName, emailAddress, DOB, address, cityTown, country)
        {
            this.HospitalId = hospitalId;
        }

        public bool isReviewed = false; // TODO - change to isReviewed
        public bool isValid = false;

        public bool wasReviewed()
        {
            return this.isReviewed;
        }

        public void validateAccount()
        {
            isReviewed = true;
            isValid = true;
        }

        public void invalidateAccount()
        {
            isReviewed = true;
            isValid = false;
        }

        public bool isValidAccount()
        {
            return isReviewed && isValid;
        }

        /**
		 * 
		 */
        public String HospitalId;

		/**
		 * 
		 */
		public HashSet<Request> Requests;

	}
}