using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Models
{
    public class DonationCenterPersonnelTransferObject
    {
        public String ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String EmailAddress { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public String InstituteName { get; set; }
    }
}
