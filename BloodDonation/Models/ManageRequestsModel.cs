using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class ManageRequestsModel
    {
        private List<DoctorAccountRequest> doctorAccountRequests;
        
        public ManageRequestsModel()
        {
            this.doctorAccountRequests = new List<DoctorAccountRequest>();
        }

        public void AddDoctorAccountRequest(DoctorAccountRequest newRequest)
        {
            this.doctorAccountRequests.Add(newRequest);
        }

        public List<DoctorAccountRequest> GetDoctorAccountRequests()
        {
            return this.doctorAccountRequests;
        }
    }
}