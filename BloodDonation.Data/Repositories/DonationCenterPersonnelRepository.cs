using BloodDonation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Data.Repositories
{
    public class DonationCenterPersonnelRepository
    {
        private List<DonationCenterPersonnel> myPersonnel;

        public DonationCenterPersonnelRepository()
        {
            this.myPersonnel = new List<DonationCenterPersonnel>();
            this.myPersonnel = new List<DonationCenterPersonnel>();
            DonationCenterPersonnel newDoc = new DonationCenterPersonnel("Pop", "Gigel", "gigelp@cjDonation.com", new DateTime(), "N/A", "Cluj", "Romania");
            newDoc.ID = "1";
            this.myPersonnel.Add(newDoc);
            newDoc = new DonationCenterPersonnel("Ion", "Ionel", "ionel@donationCenter.com", new DateTime(), "N/A", "Cluj", "Romania");
            newDoc.ID = "2";
            this.myPersonnel.Add(newDoc);
            newDoc = new DonationCenterPersonnel("George", "Georgel", "geogeo@donateNow.com", new DateTime(), "N/A", "Cluj", "Romania");
            newDoc.ID = "3";
            this.myPersonnel.Add(newDoc);
        }

        public List<DonationCenterPersonnel> findAll()
        {
            return this.myPersonnel;
        }
    }
}
