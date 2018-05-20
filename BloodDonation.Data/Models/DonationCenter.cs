
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
	public class DonationCenter : BaseEntity
	{
        public string location { get; set; }
        public string name { get; set; }
        public HashSet<Personnel> Personnel { get; set; }
        public HashSet<StoredBlood> StoredBlood { get; set; }

        public DonationCenter(){}
    }
}