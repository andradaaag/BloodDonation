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
        private const string CHILD = "donations";


        public List<Donation> FindUnresolved()
        {
            return firebaseClient
                .Child(CHILD)
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
                .Child(CHILD)
                .OrderByKey()
                .OnceAsync<Donation>()
                .Result
                .AsEnumerable()
                .Select(i => FirebaseToObject.Donation(i))
                .ToList();
        }

        public async Task FirebaseAdd(DonationCenter dc)
        {
            var res = await firebaseClient.Child("donationcenters").PostAsync(dc);
            var newDc = res.Object;
            newDc.ID = res.Key;
            firebaseClient
                .Child("donationcenters")
                .Child(res.Key)
                .PutAsync(newDc);
        }

        public void Add(Donation d)
        {
            DonationCenter don = new DonationCenter();
            DonationCenterPersonnel personnel = new DonationCenterPersonnel();
            personnel.firstName = "First";
            personnel.lastName = "Last";
            personnel.isApproved = true;
            personnel.DonationCenter = new HashSet<DonationCenter>();
            personnel.DonationCenter.Add(don);
            personnel.emailAddress = "First.Last@mail.com";

            don.location = "Cluj";
            don.name = "Donation Center Cluj";
            FirebaseAdd(don);
            
            firebaseClient
               .Child(CHILD)
               .PostAsync(d);
        }
        public void Edit(Donation d)
        {
            firebaseClient
                .Child(CHILD)
                .Child(d.ID)
                .PutAsync(d);
        }
        public Donation GetOne(string id)
        {
            return FirebaseToObject.Donation(firebaseClient
             .Child(CHILD)
             .OrderByKey()
             .StartAt(id)
             .LimitToFirst(1)
             .OnceAsync<Donation>()
             .Result
             .First());
        }
    }
}
