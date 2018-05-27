using BloodDonation.Data.Mapper;
using BloodDonation.Data.Models;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace BloodDonation.Data.Repositories
{
    public class RequestRepository
    {
        private FirebaseClient firebaseClient = new FirebaseClient("https://blooddonation-bc0b9.firebaseio.com/");
        private FirebaseToObject FirebaseToObject = new FirebaseToObject();

        public RequestRepository() { }

        public void removeAll()
        {
            firebaseClient
                .Child("requests")
                .DeleteAsync();
        }


        public List<Request> FindAll()
        {
            return firebaseClient
                .Child("requests")
                .OrderByKey()
                .OnceAsync<Request>()
                .Result
                .AsEnumerable()
                .Select(i => FirebaseToObject.Request(i))
                .ToList();
        }
        public void Save(Request r)
        {
            firebaseClient
               .Child("requests")
               .PostAsync(r);
        }
        public void EditStatus(string id,Status s)
        {
            Request r = GetOne(id);
            r.status = s;

            firebaseClient
                .Child("requests")
                .Child(r.ID)
                .PutAsync(r);
        }
        public Request GetOne(string id)
        {
            try
            {
                return FirebaseToObject.Request(firebaseClient
                 .Child("requests")
                 .OrderByKey()
                 .StartAt(id)
                 .LimitToFirst(1)
                 .OnceAsync<Request>()
                 .Result
                 .First());
            }
            catch(System.InvalidOperationException e)
            {
                return null;
            }
        }

        public List<Request> GetRentalByDoctorId(string doctorId)
        {
            try
            {
                return firebaseClient
                .Child("requests")
                .OrderBy("doctorId")
                .EqualTo(doctorId)
                .OnceAsync<Request>()
                .Result
                .AsEnumerable()
                .Select(i => FirebaseToObject.Request(i))
                .ToList();
            }
            catch (Exception e)
            {
                return new List<Request>();
            }
        }
    }
}
