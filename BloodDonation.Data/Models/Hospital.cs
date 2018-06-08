
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
/**
 * 
 */
	public class Hospital : BaseEntity
	{

		public Hospital(){}

        public String location { get; set; }
        public String name { get; set; }
        public Double lat { get; set; }
        public Double lon { get; set; }

        /**
		 * 
		 */
        public Hospital(string location, string name, Double lat, Double lon)
		{
            this.location = location;
            this.name = name;
            this.lat = lat;
            this.lon = lon;
		}


	}
}