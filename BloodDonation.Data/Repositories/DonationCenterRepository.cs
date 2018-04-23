using BloodDonation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Data.Repositories
{
    public class DonationCenterRepository
    {
        private List<DonationCenter> donationCenters;

        public DonationCenterRepository()
        {
            this.donationCenters = new List<DonationCenter>();

            DonationCenter dc = new DonationCenter("Cluj-Napoca", "Fii erou!");
            dc.ID = "1";
            this.donationCenters.Add(dc);

            dc = new DonationCenter("Brasov", "Hai la ace!");
            dc.ID = "2";
            this.donationCenters.Add(dc);

            dc = new DonationCenter("Pitesti", "Primim sange");
            dc.ID = "3";
            this.donationCenters.Add(dc);

            dc = new DonationCenter("Vaslui", "Sange pentru rachiu");
            dc.ID = "4";
            this.donationCenters.Add(dc);
        }

        public List<DonationCenter> findAll()
        {
            return this.donationCenters;
        }
    }
}
