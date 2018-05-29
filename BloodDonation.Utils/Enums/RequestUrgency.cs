using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Utils.Enums
{

        public enum DoctorUrgencyLevel
        {
            /// What the doctor can set
            Low,
            Medium,
            High
        }

        public enum PersonnelUrgencyLevel
        {
            // has donated blood before
            Donor,
            // someone has donated bood for the patient cnp
            HasDonations,
            // patient has enough donated blood 
            DonationsFulfilled
        }

   
}
