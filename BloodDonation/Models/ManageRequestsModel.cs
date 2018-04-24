using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class ManageRequestsModel
    {
        private List<DoctorAccountRequest> doctorAccountRequests;
        private List<PersonnelAccountRequest> personnelAccountRequests;
        
        public ManageRequestsModel()
        {
            this.doctorAccountRequests = new List<DoctorAccountRequest>();
            this.personnelAccountRequests = new List<PersonnelAccountRequest>();
        }

        public void AddDoctorAccountRequest(DoctorAccountRequest newRequest)
        {
            this.doctorAccountRequests.Add(newRequest);
        }

        public void AddPersonnelAccountRequest(PersonnelAccountRequest newRequest)
        {
            this.personnelAccountRequests.Add(newRequest);
        }

        public List<DoctorAccountRequest> GetDoctorAccountRequests()
        {
            return this.doctorAccountRequests;
        }

        public List<PersonnelAccountRequest> GetPersonnelAccountRequests()
        {
            return this.personnelAccountRequests;
        }
    }
}