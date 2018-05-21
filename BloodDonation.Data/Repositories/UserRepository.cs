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
    public class UserRepository
    {
        private FirebaseClient firebaseClient = new FirebaseClient("https://blooddonation-bc0b9.firebaseio.com/");
        private FirebaseToObject FirebaseToObject = new FirebaseToObject();

        public string GetRole(string UID)
        {
            return firebaseClient
                .Child("users")
                .OrderByKey()
                .EqualTo(UID)
                .OnceAsync<UserFirebase>()
                .Result
                .First()
                .Object
                .role;
        }
    }
}
