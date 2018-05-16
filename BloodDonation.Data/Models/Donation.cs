
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
/**
 * 
 */
	public class Donation : BaseEntity
	{

		public Donation()
		{
		}

		public BloodType BloodType;

		public string DonorId;

		public Stage Stage;

		public int Quantity;

        public int Plasma;
        public int RBC;
        public int Thrombocytes;

        public bool Hiv { get; set; }
        public bool HepatitisB { get; set; }
        public bool HepatitisC { get; set; }
        public bool Syphilis { get; set; }
        public bool Htlv { get; set; }
        public bool Alt { get; set; }

        public long DonationTime { get; set; }
      
    }
}
 