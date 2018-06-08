
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
        public Double Lat { get; set; }
        public Double Lon { get; set; }

		public DonationCenter(string location, string name, Double Lat, Double Lon)
		{
            this.location = location;
            this.name = name;
            this.Lat = Lat;
            this.Lon = Lon;
		}
        public DonationCenter(){}
    }
}