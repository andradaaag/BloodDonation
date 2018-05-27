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
        private const string CHILD = "requests";

        public RequestRepository() { }

        public void removeAll()
        {
            firebaseClient
                .Child(CHILD)
                .DeleteAsync();
        }



        public List<Request> FindAll()
        {
            return firebaseClient
                .Child(CHILD)
                .OrderByKey()
                .OnceAsync<Request>()
                .Result
                .AsEnumerable()
                .Select(i => FirebaseToObject.Request(i))
                .ToList();
        }

        public List<Request> GetUnsolvedRequests()
        {
            return firebaseClient
                .Child(CHILD)
                .OrderBy("status")
                .EqualTo(0)
                .OnceAsync<Request>()
                .Result
                .AsEnumerable()
                .Select(i => FirebaseToObject.Request(i))
                .ToList();
        }

        public List<Request> GetRequestByDonationCenter(string donationCenterID)
        {
            // Get all the requests taken by given donation center
            List<Request> donationCenterRequests = firebaseClient
                .Child(CHILD)
                .OrderBy("source")
                .EqualTo(donationCenterID)
                .OnceAsync<Request>()
                .Result
                .AsEnumerable()
                .Select(x => FirebaseToObject.Request(x))
                .ToList();

            // Ignore requests that are either Denied or Completed
            int i = 0;
            Request current;
            while(i < donationCenterRequests.Count)
            {
                current = donationCenterRequests[i];
                if (current.status == Status.Denied || current.status == Status.Completed)
                    donationCenterRequests.RemoveAt(i);
                else
                    i++;
            }



            return donationCenterRequests;
        }

        public void Save(Request r)
        {
            firebaseClient
                .Child(CHILD)
                .PostAsync(r);
        }


        public void Add(Request r)

        {
            firebaseClient
                .Child(CHILD)
                .PostAsync(r);
        }

        public void EditStatus(string id, Status s)
        {
            Request r = GetOne(id);
            r.status = s;

            firebaseClient
                .Child(CHILD)
                .Child(r.ID)
                .PutAsync(r);
        }

        public Request GetOne(string id)
        {
            try
            {
                return FirebaseToObject.Request(firebaseClient
                    .Child(CHILD)
                    .OrderByKey()
                    .StartAt(id)
                    .LimitToFirst(1)
                    .OnceAsync<Request>()
                    .Result
                    .First());
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }

        public List<Request> GetRentalByDoctorId(string doctorId)
        {
            try
            {
                return firebaseClient
                .Child(CHILD)
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