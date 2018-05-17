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
    public class DonationRepository
    {
        private FirebaseClient firebaseClient = new FirebaseClient("https://blooddonation-bc0b9.firebaseio.com/");
        private FirebaseToObject FirebaseToObject = new FirebaseToObject();


        public List<Donation> FindUnresolved()
        {
            return firebaseClient
                .Child("donations")
                .OrderBy("Stage")
                .StartAt(0)
                .EndAt(2)
                .OnceAsync<Donation>()
                .Result
                .AsEnumerable()
                .Select(i=>FirebaseToObject.Donation(i))
                .ToList();
        }
        
        public List<Donation> FindAll()
        {
            return firebaseClient
                .Child("donations")
                .OrderByKey()
                .OnceAsync<Donation>()
                .Result
                .AsEnumerable()
                .Select(i => FirebaseToObject.Donation(i))
                .ToList();
        }
        public void Add(Donation d)
        {
            firebaseClient
               .Child("donations")
               .PostAsync(d);
        }
        public void Edit(Donation d)
        {
            firebaseClient
                .Child("donations")
                .Child(d.ID)
                .PutAsync(d);
        }
        public Donation GetOne(string id)
        {
            return FirebaseToObject.Donation(firebaseClient
             .Child("donations")
             .OrderByKey()
             .StartAt(id)
             .LimitToFirst(1)
             .OnceAsync<Donation>()
             .Result
             .First());
        }
    }
}
