﻿using BloodDonation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using BloodDonation.Data.Mapper;

namespace BloodDonation.Data.Repositories
{
    public class DonationCenterPersonnelRepository
    {
        private FirebaseClient firebaseClient = new FirebaseClient("https://blooddonation-bc0b9.firebaseio.com/");
        private FirebaseToObject FirebaseToObject = new FirebaseToObject();
        private const string CHILD = "personnel";

        public bool exists(string id)
        {
            return firebaseClient
                       .Child(CHILD)
                       .OrderByKey()
                       .EqualTo(id)
                       .OnceAsync<DonationCenterPersonnel>()
                       .Result.AsEnumerable().Count() == 1;
        }

        public void Save(DonationCenterPersonnel dcp)
        {
            firebaseClient
                .Child(CHILD)
                .Child(dcp.ID)
                .PutAsync(dcp);
        }




        public void Edit(DonationCenterPersonnel d)

        {
            firebaseClient
                .Child(CHILD)
                .Child(d.ID)
                .PutAsync(d);
        }


        public List<DonationCenterPersonnel> findAll()
        {
            return firebaseClient
                .Child(CHILD)
                .OrderByKey()
                .OnceAsync<DonationCenterPersonnel>()
                .Result
                .AsEnumerable()
                .Select(i => FirebaseToObject.DonationCenterPersonnel(i))
                .ToList();
        }


        public void deleteForId(string id)
        {
            firebaseClient.Child(CHILD).Child(id).DeleteAsync();
        }

        public DonationCenterPersonnel GetOne(string id)
        {
            try
            {
                return firebaseClient
                    .Child(CHILD)
                    .OrderByKey()
                    .EqualTo(id)
                    .OnceAsync<DonationCenterPersonnel>()
                    .Result
                    .AsEnumerable()
                    .Select(i => FirebaseToObject.DonationCenterPersonnel(i))
                    .First();
            }catch(Exception ex)
            {
                return null;
            }
        }
    }
}