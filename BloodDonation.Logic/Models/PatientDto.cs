using BloodDonation.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Models
{
    public class PatientDto
    {
        public String Id { get; set; }
        public String Cnp { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int BloodQuantity { get; set; }
        public PatientStatus PatientStatus { get; set; }
    }
}
