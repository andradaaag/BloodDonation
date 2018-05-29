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
        private const string CHILD = "storedblood";

        public List<StoredBlood> FindAll()
        {
            return firebaseClient
                .Child(CHILD)
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
               .Child(CHILD)
               .PostAsync(d);

        }
        public void Edit(StoredBlood d)
        {
            firebaseClient
                .Child(CHILD)
                .Child(d.ID)
                .PutAsync(d);
        }

        public StoredBlood GetOne(string id)
        {
            return firebaseClient
                 .Child(CHILD)
                 .OrderBy("ID")
                 .OnceAsync<StoredBlood>()
                 .Result
                 .AsEnumerable()
                 .Select(i => FirebaseToObject.StoredBlood(i))
                 .ToList()
                 .First();
        }

        public List<StoredBlood> FindAllByDonationCenter(string donCenter)
        {
            return firebaseClient
                .Child(CHILD)
                .OrderBy("DonationCenterID")
                .EqualTo(donCenter)
                .OnceAsync<StoredBlood>()
                .Result
                .Select(i => FirebaseToObject.StoredBlood(i))
                .ToList();
        }
    }
}