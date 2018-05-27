using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Models
{
    public class RequestPersonnel: BaseEntity
    {
        public RequestPersonnel() { }
        public Status status;

        public String destination;      //hospitalid
        public String source;           //donationcenterid
        public String doctorId;
        public String patientCnp;


        public String hospitalName;
        public String hospitalLocation;

        public String doctorName;

        public int quantity;
        public BloodType bloodType;

        public void setStatus(string s)
        {
            status = (Status)Enum.Parse(typeof(Status), s);
        }

        override
        public string ToString()
        {
            return status + "  " + hospitalName + "  " + hospitalLocation + "  " + quantity + "  " + bloodType.Group + "  " + bloodType.RH;
        }
    }
}
