
using BloodDonation.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
/**
 * 
 */
	public class Patient : BaseEntity
	{

        public String Cnp;
        public String FirstName;
        public String LastName;
        public int BloodQuantity;
        public PatientStatus PatientStatus;

		public Patient(){}

        public Patient(String cnp, String firstName, String lastName, int bloodQuantity, PatientStatus patientStatus)
        {
            this.Cnp = cnp;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BloodQuantity = bloodQuantity;
            this.PatientStatus = patientStatus;
        }

    }
}