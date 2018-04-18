
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
/**
 * 
 */
	public class Request : BaseEntity
	{

		/**
		 * 
		 */
		public Request()
		{
		}

		/**
		 * 
		 */
		public Status status;

		/**
		 * 
		 */
		public DonationCenter source;

		/**
		 * 
		 */
		public Hospital destination;

		/**
		 * 
		 */
		public int quantity;

		/**
		 * 
		 */
		public BloodType bloodType;




	}
}