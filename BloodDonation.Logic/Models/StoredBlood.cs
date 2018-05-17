using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Models
{
    public class StoredBlood
    {
        public string ID;

        public BloodType BloodType;

        public int Amount { get; set; }

        public long CollectionDate;

        public Enum Component;
    }
}
