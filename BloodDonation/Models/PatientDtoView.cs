using BloodDonation.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class PatientDtoView
    {

        public String Id;
        public String Cnp;
        public String FirstName;
        public String LastName;
        public int BloodQuantity;
        public PatientStatus PatientStatus;

    }
}