
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

        public Donor donor { get; private set; }

        public string DonorCnp;

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

        public string DonationCenterID { get; set; }

        public long DonationTime { get; set; }
      
		public String testResult;
		

		public DonationCenter center;

		public String donationDate;
        public String donationHour;

		/**
		 * 
		 */
		public int quantity;

		public Donation(BloodType bloodType, Donor donor, Stage stage, int quantity, String testResult)
		{
			this.BloodType = bloodType;
			this.donor = donor;
			this.Stage = stage;
			this.quantity = quantity;
			this.testResult = testResult;
		}
	}
}
