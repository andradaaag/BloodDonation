
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
        public HashSet<string> Personnel { get; set; }
        public HashSet<string> StoredBlood { get; set; }
		public DonationCenter(string location, string name)
		{
            this.location = location;
            this.name = name;
		}
        public DonationCenter(){}
    }
}