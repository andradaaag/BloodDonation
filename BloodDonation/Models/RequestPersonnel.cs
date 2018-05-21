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
        public string hospitalName;
        public string hospitalLocation;
        public int quantity;
        public BloodType bloodType;
        public string tempStatus = "BeingProcessed";

        public void setStatus(string s)
        {
            status = (Status)Enum.Parse(typeof(Status), s);
        }

        override
        public string ToString()
        {
            return status + "  " + hospitalName + "  " + hospitalLocation + "  " + quantity + "  " + bloodType.Group + "  " + bloodType.PH;
        }
    }
}
