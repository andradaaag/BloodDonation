using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Data.Mapper;
using BloodDonation.Data.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace BloodDonation.Data.Repositories
{
    public class DoctorRepository
    {
        //TODO - here it will be linked with firebase, rn just a mock repo

        private FirebaseClient firebaseClient = new FirebaseClient("https://blooddonation-bc0b9.firebaseio.com/");
        private FirebaseToObject FirebaseToObject = new FirebaseToObject();
        private const string CHILD = "doctors";

        /*
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
            newDoc = new Doctor("Doctorescu", "Doc", "docty@cjHospital.com", new DateTime(), "N/A", "Cluj", "Romania");
            newDoc.ID = "4";
            newDoc.validateAccount();
            this.myDoctors.Add(newDoc);
            newDoc = new Doctor("Stirbu", "Claudia", "claudiaStb@docts.com", new DateTime(), "N/A", "Sibiu", "Romania");
            newDoc.ID = "5";
            newDoc.validateAccount();
            this.myDoctors.Add(newDoc);
        }
        */

        public void Save(Doctor newDoctor)
        {
            firebaseClient
                .Child(CHILD)
                .Child(newDoctor.ID)
                .PutAsync(newDoctor);
        }

        public List<Doctor> findAll()
        {
            return firebaseClient
                .Child(CHILD)
                .OrderByKey()
                .OnceAsync<Doctor>()
                .Result
                .AsEnumerable()
                .Select(i => FirebaseToObject.Doctor(i))
                .ToList();
        }


    }
}
