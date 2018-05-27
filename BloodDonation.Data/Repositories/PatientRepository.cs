using BloodDonation.Data.Mapper;
using BloodDonation.Data.Models;
using BloodDonation.Utils.Enums;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Data.Repositories
{
    public class PatientRepository
    {
        private FirebaseClient firebaseClient = new FirebaseClient("https://blooddonation-bc0b9.firebaseio.com/");
        private FirebaseToObject FirebaseToObject = new FirebaseToObject();
        private const string CHILD = "patients";

        public PatientRepository()
        {
            Patient p = new Patient("118932salveaza-lacum","Grigore","Nemuritoru",100,PatientStatus.AwaitingBlood);
            this.Save(p);
            p = new Patient("cu30degrademaitarecavodka", "Alexandru", "Razboinicu", 2, PatientStatus.Dead);
            this.Save(p);
            p = new Patient("0742117923", "Peni", "Samuraiul", 12, PatientStatus.AwaitingBlood);
            this.Save(p);
        }

        public List<Patient> FindAll()
        {
            return firebaseClient
                    .Child(CHILD)
                    .OrderByKey()
                    .OnceAsync<Patient>()
                    .Result
                    .AsEnumerable()
                    .Select(i => FirebaseToObject.Patient(i))
                    .ToList();
        }

        public void Save(Patient newpatient)
        {
            firebaseClient
                .Child(CHILD)
                .Child(newpatient.ID)
                .PutAsync(newpatient);
        }

        public void Edit(Patient newpatient)
        {
            firebaseClient
                .Child(CHILD)
                .Child(newpatient.ID)
                .PutAsync(newpatient);
        }

        public Patient FindByCnp(String cnp)
        {
            return firebaseClient
                    .Child(CHILD)
                    .OrderBy("cnp")
                    .EqualTo(cnp)
                    .OnceAsync<Patient>()
                    .Result
                    .AsEnumerable()
                    .Select(i => FirebaseToObject.Patient(i))
                    .First(); 
        }
    }
}
