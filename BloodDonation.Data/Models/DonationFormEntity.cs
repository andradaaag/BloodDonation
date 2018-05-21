using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonation.Data.Models
{
/**
 * 
 */
    public class DonationFormEntity : BaseEntity
    {
        /**
         * 
         */
        public DonationFormEntity()
        {
        }

        /**
         * 
         */
        public int currentWeight;

        public DonationFormEntity(int currentWeight)
        {
            this.currentWeight = currentWeight;
        }
    }
}