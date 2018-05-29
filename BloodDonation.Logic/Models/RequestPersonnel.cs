using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Models
{
    public class RequestPersonnel: BaseEntity
    {
        public RequestPersonnel() { }

        public Status status;

        public string destination;      //hospitalid
        public string source;           //donationcenterid
        public string doctorId;
        public string patientCnp;

        public int quantity;
        public BloodType bloodType;
        public RequestComponent component;
    }
}
