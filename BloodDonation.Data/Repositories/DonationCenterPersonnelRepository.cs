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
        private List<DonationCenterPersonnel> myDonationCenterPersonnel;

        public DonationCenterPersonnelRepository()
        {
            this.myDonationCenterPersonnel = new List<DonationCenterPersonnel>();

            DonationCenterPersonnel newDcP = new DonationCenterPersonnel("Moldovan", "Ioana", "ioanaM@dccluj.com", new DateTime(), "N/A", "Cluj", "Romania");
            newDcP.ID = "1";
            this.myDonationCenterPersonnel.Add(newDcP);
            newDcP = new DonationCenterPersonnel("Georgescu", "Dan", "danGeo@personnel.com", new DateTime(), "N/A", "Pitesti", "Romania");
            newDcP.ID = "2";
            this.myDonationCenterPersonnel.Add(newDcP);
            newDcP = new DonationCenterPersonnel("Tudor", "Bogdan", "tdoricaBogdi@doctorlife.com", new DateTime(), "N/A", "Cluj", "Albania");
            newDcP.ID = "3";
            this.myDonationCenterPersonnel.Add(newDcP);

            newDcP = new DonationCenterPersonnel("Mihoc", "Balaur", "micky@dhcp.com", new DateTime(), "N/A", "Timisoara", "Romania");
            newDcP.ID = "4";
            newDcP.validateAccount();
            this.myDonationCenterPersonnel.Add(newDcP);
            newDcP = new DonationCenterPersonnel("Aurelianus", "Maximus", "latinoBabe@dausange.com", new DateTime(), "N/A", "Vaslui", "Romania");
            newDcP.ID = "5";
            newDcP.validateAccount();
            this.myDonationCenterPersonnel.Add(newDcP);

        }

        public List<DonationCenterPersonnel> findAll()
        {
            return this.myDonationCenterPersonnel;
        }

    }
}
