using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class DonationListModel
    {
        public DonationListModel()
        {

        }

        public List<DonationModel> Donations { set; get; } = new List<DonationModel>();


    }
}