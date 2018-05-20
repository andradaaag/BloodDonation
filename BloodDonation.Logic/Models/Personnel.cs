using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Models
{
    public class Personnel : User
    {
        public bool isApproved { get; set; }

        public Personnel() : base("N/A", "N/A", "N/A", DateTime.Now, "N/A", "N/A", "N/A")
        {
            isApproved = false;
        }

        public Personnel(string firstName, string lastName, string emailAddress, DateTime DOB, string address, string cityTown, string country)
            : base(firstName, lastName, emailAddress, DOB, address, cityTown, country)
        {
            isApproved = false;
        }
    }
}
