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
        public async Task<List<Donation>> FindAll()
        {
            var dons = await firebaseClient.Child("donations")
                .OrderByKey()
                .OnceAsync<Donation>();

            var l = new List<Donation>();

            foreach (var d in dons)
            {
                l.Add(d.Object);
            }

            return l;
        }
        public async Task Add(Donation d)
        {
           
            var id = await firebaseClient.Child("donations").PostAsync(d);

        }
    }
}
