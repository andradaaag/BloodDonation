using BloodDonation.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class ManageAccountsModel
    {
        //SEARCHING PART
        public string SearchQuery { get; set; }
        public bool IsViewingSearchResults { get; set; }

        //LISTING PART
        private List<DoctorDisplayData> doctorAccountRequests; // also used for valid accounts - TODO rename
        private List<DonationCenterPersonnelDisplayData> donationCenterPersonnelAccountRequests;

        public ManageAccountsModel()
        {
            this.doctorAccountRequests = new List<DoctorDisplayData>();
            this.donationCenterPersonnelAccountRequests = new List<DonationCenterPersonnelDisplayData>();
            this.IsViewingSearchResults = false;
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

        public void ResetDoctorAccounts()
        {
            this.doctorAccountRequests.Clear();
        }

        public void ResetDonationCenterPersonnelAccounts()
        {
            this.donationCenterPersonnelAccountRequests.Clear();
        }
    }
}