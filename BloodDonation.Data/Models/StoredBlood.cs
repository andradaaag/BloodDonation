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
        /**
         * 
         */
        public StoredBlood()
        {
        }

        /**
         * 
         */
        public BloodType bloodType;

        /**
         * 
         */
        public DateTime collectionDate;

        /**
         * 
         */
        public Enum component;
    }
}