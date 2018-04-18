using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Data.Models;

namespace BloodDonation.Data.Repositories
{
    public class DoctorRepository
    {
        //TODO - here it will be linked with firebase, rn just a mock repo

        private List<Doctor> myDoctors;

        public DoctorRepository()
        {
            this.myDoctors = new List<Doctor>();
            Doctor newDoc = new Doctor("Popescu", "Marta", "martap@cjHospital.com", new DateTime(), "N/A", "Cluj", "Romania");
            newDoc.ID = "1";
            this.myDoctors.Add(newDoc);
            newDoc = new Doctor("Grigorescu", "Mihail", "mihailGrig@docts.com", new DateTime(), "N/A", "Cluj", "Romania");
            newDoc.ID = "2";
            this.myDoctors.Add(newDoc);
            newDoc = new Doctor("Mihailescu", "Ounicea", "ouniceaMih@doctorlife.com", new DateTime(), "N/A", "Cluj", "Albania");
            newDoc.ID = "3";
            this.myDoctors.Add(newDoc);
        }


        public List<Doctor> findAll()
        {
            return this.myDoctors;
        }


    }
}
