﻿using BloodDonation.Data.Mapper;
using BloodDonation.Data.Models;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;
using BloodDonation.Utils.Enums;

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

        public void DeleteById(String id)
        {
            firebaseClient
                .Child("requests")
                .Child(id)
                .DeleteAsync();

        }

        public List<Request> FindAll()
        {
            try { 
            return firebaseClient
                .Child("requests")
                .OrderByKey()
                .OnceAsync<Request>()
                .Result
                .AsEnumerable()
                .Select(i => FirebaseToObject.Request(i))
                .ToList();
            }
            catch (Exception ex)
            {
                return new List<Request>();
            }

        }

        public List<Request> GetUnsolvedRequests()
        {
            try
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
            catch(Exception ex)
            {
                return new List<Request>();
            }
        }

        public List<Request> GetRequestsInProgress(string donationCenterID)
        {
            // Get all the requests taken by given donation center
            try
            {
                return firebaseClient
                    .Child(CHILD)
                    .OrderBy("source")
                    .EqualTo(donationCenterID)
                    .OnceAsync<Request>()
                    .Result
                    .AsEnumerable()
                    .Select(x => FirebaseToObject.Request(x))
                    .Where(x=> x.status == Status.Accepted && x.source == donationCenterID )
                    .ToList();

            }
            catch (Exception ex)
            {
                return new List<Request>();
            }

           
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

        public void Edit(Request r)
        {
            firebaseClient
                .Child(CHILD)
                .Child(r.ID)
                .PutAsync(r);
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

        public void EditSource(string id, string donationCenterID)
        {
            Request r = GetOne(id);
            r.source = donationCenterID;
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
            }catch(Exception ex)
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
            catch(Exception ex)
            {
                return new List<Request>();
            }

        }
        public List<Request> GetRequestByPatientCnp(string patientCnp)
        {
            try
            {
                return firebaseClient
                    .Child("requests")
                    .OrderBy("patientCnp")
                    .EqualTo(patientCnp)
                    .OnceAsync<Request>()
                    .Result
                    .AsEnumerable()
                    .Select(i => FirebaseToObject.Request(i))
                    .ToList();
            }
            catch(Exception ex)
            {
                return new List<Request>();
            }

        }

    }
}