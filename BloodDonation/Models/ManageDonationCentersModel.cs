﻿using System;
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

        //Searching part

        public string SearchName { get; set; }
        public string SearchLocation { get; set; }
        public bool IsViewingSearchResults { get; set; }

        //Listing part
        private List<DonationCenterDisplayData> donationCenterDisplayDatas;

        public ManageDonationCentersModel()
        {
            this.donationCenterDisplayDatas = new List<DonationCenterDisplayData>();
            this.IsViewingSearchResults = false;
        }

        public void AddDonationCenter(DonationCenterDisplayData dcdd)
        {
            this.donationCenterDisplayDatas.Add(dcdd);
        }

        public List<DonationCenterDisplayData> GetDonationCenters()
        {
            return this.donationCenterDisplayDatas;
        }

        public List<string> GetDonationsCenterName()
        {
            return donationCenterDisplayDatas
                                   .Distinct()
                                   .AsEnumerable()
                                   .Select(center =>center.Name)
                                   .ToList();
        }

        public void ResetDonationCenters()
        {

        }
    }
}