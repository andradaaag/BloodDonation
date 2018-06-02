using BloodDonation.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Models
{
    public class RequestPersonnel: BaseEntity
    {


        public Status status { get; set; }

        public string destination { get; set; }      //hospitalid
        public string source { get; set; }           //donationcenterid
        public string doctorId { get; set; }
        public string patientCnp { get; set; }

        public int quantity { get; set; }
        public BloodType bloodType { get; set; }

        public DoctorUrgencyLevel urgency { get; set; }

        public RequestPersonnel()
        {
            this.bloodType = new BloodType();
        }
    }
}
