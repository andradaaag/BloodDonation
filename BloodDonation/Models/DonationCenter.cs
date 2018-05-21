using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Models
{
    public class DonationCenter : BaseEntity
    {
        public string location { get; set; }
        public string name { get; set; }
        public HashSet<Personnel> Personnel { get; set; }
        public HashSet<StoredBloodModel> StoredBlood { get; set; }

        public DonationCenter() { }
    }
}
