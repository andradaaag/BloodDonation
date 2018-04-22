using BloodDonation.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class ManageAccountsModel
    {
        private List<DoctorDisplayData> doctorAccountRequests;
        private List<DonationCenterPersonnelDisplayData> donationCenterPersonnelAccountRequests;

        public ManageAccountsModel()
        {
            this.doctorAccountRequests = new List<DoctorDisplayData>();
            this.donationCenterPersonnelAccountRequests = new List<DonationCenterPersonnelDisplayData>();
        }

        public void AddDoctorAccount(DoctorDisplayData newDoctor)
        {
            this.doctorAccountRequests.Add(newDoctor);
        }

        public List<DoctorDisplayData> GetDoctorAccounts()
        {
            return this.doctorAccountRequests;
        }

        public void AddDonationCenterPersonnelAccount(DonationCenterPersonnelDisplayData newRequest)
        {
            this.donationCenterPersonnelAccountRequests.Add(newRequest);
        }

        public List<DonationCenterPersonnelDisplayData> GetDonationCenterPersonnelAccounts()
        {
            return this.donationCenterPersonnelAccountRequests;
        }
    }
}