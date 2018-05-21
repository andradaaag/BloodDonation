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
        public string hospitalName;
        public string hospitalLocation;
        public int quantity;
        public BloodType bloodType;
    }
}
