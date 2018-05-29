using BloodDonation.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
/**
 * 
 */
    public class StoredBlood : BaseEntity
    {
        public StoredBlood()
        {
        }

        public BloodType BloodType;

        public int Amount { get; set; }

        public long CollectionDate;

        public Component Component;

        public string DonationCenterID { get; set; }
    }
}