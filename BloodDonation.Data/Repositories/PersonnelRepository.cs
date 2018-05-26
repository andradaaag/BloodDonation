

﻿using BloodDonation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using Firebase.Database.Query;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Data.Mapper;
using System.IO;

namespace BloodDonation.Data.Repositories
{
    public class PersonnelRepository
    {
      
        private FirebaseClient firebaseClient = new FirebaseClient("https://blooddonation-bc0b9.firebaseio.com/");
        private FirebaseToObject FirebaseToObject = new FirebaseToObject();
        private static string CHILD = "personnel";

        public PersonnelRepository()
        {
        }
        public List<DonationCenterPersonnel> FindAll()
        {
            return firebaseClient
                .Child(CHILD)
                .OrderByKey()
                .OnceAsync<DonationCenterPersonnel>()
                .Result
                .AsEnumerable()
                .Select(i => FirebaseToObject.Personnel(i))
                .ToList();
        }

        public void Add(DonationCenterPersonnel d)
        {
            firebaseClient
               .Child(CHILD)
               .PostAsync(d);
        }
       

    }
}
