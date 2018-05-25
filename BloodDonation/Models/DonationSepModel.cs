using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class DonationSepModel
    {
        public DonationSepModel()
        {

        }

        public List<DonationModel> Donations { set; get; } = new List<DonationModel>();
        public List<StoredBloodModel> StoredBlood { set; get; } = new List<StoredBloodModel>();
    }
}