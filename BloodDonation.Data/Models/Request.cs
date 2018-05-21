
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
	public class Request : BaseEntity
	{
		public Request(){}

		public Status status { get; set; }
        public DonationCenter source { get; set; }
        public Hospital destination { get; set; }
		public int quantity { get; set; }
        public BloodType bloodType { get; set; }
    }
}