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
    public class StoredBloodRepository
    {
        private FirebaseClient firebaseClient = new FirebaseClient("https://blooddonation-bc0b9.firebaseio.com/");
        private FirebaseToObject FirebaseToObject = new FirebaseToObject();
        private string Child = "StoredBlood";


        //public List<StoredBlood> FindUnresolved()
        //{
        //    return firebaseClient
        //        .Child(Child)
        //        .OrderBy("Stage")
        //        .StartAt(0)
        //        .EndAt(2)
        //        .OnceAsync<Donation>()
        //        .Result
        //        .AsEnumerable()
        //        .Select(i => FirebaseToObject.Donation(i))
        //        .ToList();
        //}

        public List<StoredBlood> FindAll()
        {
            return firebaseClient
                .Child(Child)
                .OrderByKey()
                .OnceAsync<StoredBlood>()
                .Result
                .AsEnumerable()
                .Select(i => FirebaseToObject.StoredBlood(i))
                .ToList();
        }
        public void Add(StoredBlood d)
        {
            firebaseClient
               .Child(Child)
               .PostAsync(d);

        }
        public void Edit(StoredBlood d)
        {
            firebaseClient
                .Child(Child)
                .Child(d.ID)
                .PutAsync(d);
        }
        public StoredBlood GetOne(string id)
        {
            return firebaseClient
                 .Child(Child)
                 .OrderBy("ID")
                 .OnceAsync<StoredBlood>()
                 .Result
                 .AsEnumerable()
                 .Select(i => FirebaseToObject.StoredBlood(i))
                 .ToList()
                 .First();
        }
    }
}