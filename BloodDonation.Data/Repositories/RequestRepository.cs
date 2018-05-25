﻿using BloodDonation.Data.Mapper;
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

        public void testing()
        {

            Request a = new Request
            {
                ID = "1",
                status = Status.BeingProcessed,
                source = new DonationCenter
                {
                    location = "None",
                    name = "None"
                },
                destination = new Hospital
                {
                    location = "Street Testing, No. 15",
                    name = "Hospital St. Paul"
                },
                bloodType = new BloodType
                {
                    Group = "1",
                    RH = false
                },
                quantity = 100
            };

            Request b = new Request
            {
                ID = "2",
                status = Status.Accepted,
                source = new DonationCenter
                {
                    location = "Street Testing, No. 10",
                    name = "Donation Center A"
                },
                destination = new Hospital
                {
                    location = "Street Testing, No. 17",
                    name = "Hospital name"
                },
                bloodType = new BloodType
                {
                    Group = "1",
                    RH = false
                },
                quantity = 200
            };

            if( this.GetOne(a.ID) == null)
                this.Add(a);
            if (this.GetOne(b.ID) == null)
                this.Add(b);
        }

        public void removeAll()
        {
            firebaseClient
                .Child("requests")
                .DeleteAsync();
        }


        public RequestRepository()
        {
            //removeAll();
            testing();
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
        public void Add(Request r)
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
    }
}