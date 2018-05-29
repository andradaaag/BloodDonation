using BloodDonation.Data.Models;
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

        public Component Component;

        public string DonationCenterID { get; set; }


        /*
         * Default comparer method for .Sort() calls. Will place objects in decreasing order based on their Amount field.
         */
        public int CompareTo(StoredBlood other)
        {
            if (other == null)
                return 1;
            return other.Amount.CompareTo(this.Amount);
        }
    }
}
