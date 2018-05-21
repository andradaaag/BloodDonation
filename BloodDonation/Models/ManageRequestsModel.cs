using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class ManageRequestsModel
    {
        private List<DoctorAccountRequest> doctorAccountRequests;
        private List<DonationCenterPersonnelAccountRequest> donationCenterPersonnelAccountRequests;
        
        public ManageRequestsModel()
        {
            this.doctorAccountRequests = new List<DoctorAccountRequest>();
            this.donationCenterPersonnelAccountRequests = new List<DonationCenterPersonnelAccountRequest>();
        }

        public void AddDoctorAccountRequest(DoctorAccountRequest newRequest)
        {
            this.doctorAccountRequests.Add(newRequest);
        }

        public List<DoctorAccountRequest> GetDoctorAccountRequests()
        {
            return this.doctorAccountRequests;
        }

        public void AddDonationCenterPersonnelAccountRequest(DonationCenterPersonnelAccountRequest newRequest)
        {
            this.donationCenterPersonnelAccountRequests.Add(newRequest);
        }

        public List<DonationCenterPersonnelAccountRequest> GetDonationCenterPersonnelAccountRequests()
        {
            return this.donationCenterPersonnelAccountRequests;
        }
    }
}