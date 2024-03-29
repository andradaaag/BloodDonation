﻿using BloodDonation.Data.Mapper;
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
    public class DonorRepository
    {
        private FirebaseClient firebaseClient = new FirebaseClient("https://blooddonation-bc0b9.firebaseio.com/");
        private FirebaseToObject FirebaseToObject = new FirebaseToObject();
        private const string CHILD = "donors";


        private List<Donation> myDonations;

        public DonorRepository()
        {
         
        }

        public bool exists(string id)
        {
            return firebaseClient
                .Child(CHILD)
                .OrderByKey()
                .EqualTo(id)
                .OnceAsync<Donor>()
                .Result.AsEnumerable().Count() == 1;
        }

        public void Save(Donor newDonor)
        {
            firebaseClient
                .Child(CHILD)
                .Child(newDonor.ID)
                .PutAsync(newDonor);
        }

        public List<Donation> GetDonations()
        {
            return myDonations;
        }


        public List<Donor> GetDonors()
        {
            try
            {
                return firebaseClient
                   .Child(CHILD)
                   .OrderByKey()
                   .OnceAsync<Donor>()
                   .Result
                   .AsEnumerable()
                   .Select(i => FirebaseToObject.Donor(i))
                   .ToList();
            }
            catch (Exception ex)
            {
                return new List<Donor>();
            }
        }

        public Donor GetDonorByCNP(string CNP)
        {
            return FirebaseToObject.Donor(firebaseClient
                .Child(CHILD)
                .OrderByKey()
                .OnceAsync<Donor>()
                .Result
                .ToList()
                .Find(d=>FirebaseToObject.Donor(d).CNP==CNP));
        }

        public Donor GetOne(string id)
        {
            return firebaseClient
                .Child(CHILD)
                .OrderByKey()
                .EqualTo(id)
                .OnceAsync<Donor>()
                .Result
                .AsEnumerable()
                .Select(i => FirebaseToObject.Donor(i))
                .First();
        }

    }
}