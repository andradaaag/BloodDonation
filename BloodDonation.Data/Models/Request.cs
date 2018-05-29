
using BloodDonation.Utils.Enums;
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

        public String source { get; set; }
        public String destination { get; set; }
        public String doctorId { get; set; }
        public String patientCnp { get; set; }

        public int quantity { get; set; }

        public BloodType bloodType { get; set; }
        
        public DoctorUrgencyLevel urgency { get; set; }

    }
}