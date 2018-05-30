using BloodDonation.Data.Mapper;
using BloodDonation.Data.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Data.Repositories
{
    public class HospitalRepository
    {
        
        private FirebaseClient firebaseClient = new FirebaseClient("https://blooddonation-bc0b9.firebaseio.com/");
        private FirebaseToObject FirebaseToObject = new FirebaseToObject();
        private const string CHILD = "hospitals";

        public HospitalRepository()
        {
        }

        public List<Hospital> FindAll()
        {
            try
            {
                return firebaseClient
                        .Child(CHILD)
                        .OrderByKey()
                        .OnceAsync<Hospital>()
                        .Result
                        .AsEnumerable()
                        .Select(i => FirebaseToObject.Hospital(i))
                        .ToList();
            }catch(Exception ex)
            {
                return new List<Hospital>();
            }
        }

        public void Save(Hospital newhospital)
        {
            firebaseClient
                .Child(CHILD)
                .PostAsync(newhospital);
        }

        public void Edit(Hospital newhospital)
        {
            firebaseClient
                .Child(CHILD)
                .Child(newhospital.ID)
                .PutAsync(newhospital);
        }

        public List<Hospital> FindByName(String name)
        {
            try
            {
                return firebaseClient
                    .Child(CHILD)
                    .OrderBy("name")
                    .EqualTo(name)
                    .OnceAsync<Hospital>()
                    .Result
                    .AsEnumerable()
                    .Select(i => FirebaseToObject.Hospital(i))
                    .ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Hospital FindById(String Id)
        {
            try
            {
                return firebaseClient
                    .Child(CHILD)
                    .OrderByKey()
                    .EqualTo(Id)
                    .OnceAsync<Hospital>()
                    .Result
                    .AsEnumerable()
                    .Select(i => FirebaseToObject.Hospital(i))
                    .First();
            }
            catch(Exception ex)
            {
                return new Hospital("none","none");
            }
        }

        public void DeleteById(string id)
        {
            firebaseClient
                .Child(CHILD)
                .Child(id)
                .DeleteAsync();
        }
    }
}
