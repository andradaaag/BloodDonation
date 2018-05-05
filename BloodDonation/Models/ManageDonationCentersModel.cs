using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class ManageDonationCentersModel
    {
        //Creating part

        public string Name { get; set; }
        public string Location { get; set; }


        //Listing part
        private List<DonationCenterDisplayData> donationCenterDisplayDatas;

        public ManageDonationCentersModel()
        {
            this.donationCenterDisplayDatas = new List<DonationCenterDisplayData>();
        }

        public void AddDonationCenter(DonationCenterDisplayData dcdd)
        {
            this.donationCenterDisplayDatas.Add(dcdd);
        }

        public List<DonationCenterDisplayData> GetDonationCenters()
        {
            return this.donationCenterDisplayDatas;
        }
    }
}