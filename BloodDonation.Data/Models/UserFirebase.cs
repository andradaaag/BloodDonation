using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Data.Models
{
    public class UserFirebase
    {
        public string ID { get; set; }
        public string role { get; set; }
        public bool isApproved { get; set;}
    }
}
