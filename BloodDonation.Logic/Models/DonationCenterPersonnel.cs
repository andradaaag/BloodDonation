using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Models
{
    public class DonationCenterPersonnel : User
    {
        public bool isApproved { get; set; }
        public string DonationCenterID { get; set; }

        public DonationCenterPersonnel() : base("N/A", "N/A", "N/A", DateTime.Now, "N/A", "N/A", "N/A")
        {
            isApproved = false;
        }

        public DonationCenterPersonnel(string firstName, string lastName, string emailAddress, DateTime DOB, string address, string cityTown, string country)
            : base(firstName, lastName, emailAddress, DOB, address, cityTown, country)
        {
            isApproved = false;
        }
    }
}
