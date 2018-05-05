using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class ManageHospitalsModel
    {
        //CREATE PART
        public string Name { get; set; }
        public string Location { get; set; }

        //LISTING PART
        private List<HospitalDisplayData> hospitalDisplayDatas;

        public ManageHospitalsModel()
        {
            this.hospitalDisplayDatas = new List<HospitalDisplayData>();
        }

        public void AddHospital(HospitalDisplayData hdd)
        {
            this.hospitalDisplayDatas.Add(hdd);
        }

        public List<HospitalDisplayData> GetHospitals()
        {
            return this.hospitalDisplayDatas;
        }

    }
}