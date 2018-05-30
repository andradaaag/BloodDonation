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


        private FirebaseClient firebaseClient = new FirebaseClient("https://blooddonation-bc0b9.firebaseio.com/");
        private FirebaseToObject FirebaseToObject = new FirebaseToObject();
        private const string CHILD = "doctors";



        public bool IsIDPresent(string id)
        {
            return firebaseClient
                .Child(CHILD)
                .OrderByKey()
                .EqualTo(id)
                .OnceAsync<Doctor>()
                .Result.AsEnumerable().Count() == 1;
        }

        public void Save(Doctor newDoctor)
        {
            firebaseClient
                .Child(CHILD)
                .Child(newDoctor.ID)
                .PutAsync(newDoctor);
        }

        public List<Doctor> findAll()
        {
            try
            {
                return firebaseClient
                    .Child(CHILD)
                    .OrderByKey()
                    .OnceAsync<Doctor>()
                    .Result
                    .AsEnumerable()
                    .Select(i => FirebaseToObject.Doctor(i))
                    .ToList();
            } catch (Exception ex)
            {
                return new List<Doctor>();
            }
        }

        public void deleteForId(string id)
        {
            firebaseClient.Child(CHILD).Child(id).DeleteAsync();
        }

        public Doctor GetOne(string id)
        {
            try
            {
                return firebaseClient
                    .Child(CHILD)
                    .OrderByKey()
                    .EqualTo(id)
                    .OnceAsync<Doctor>()
                    .Result
                    .AsEnumerable()
                    .Select(i => FirebaseToObject.Doctor(i))
                    .First();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
