using BloodDonation.Data.Models;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Data.Mapper
{
    public class FirebaseToObject
    {
        public Donation Donation(FirebaseObject<Donation> donation)
        {
            Donation d = donation.Object;
            d.ID = donation.Key;
            return d;
        }

        public StoredBlood StoredBlood(FirebaseObject<StoredBlood> stored)
        {
            StoredBlood b = stored.Object;
            b.ID = stored.Key;
            return b;
        }
        public UserFirebase UserFirebase(FirebaseObject<UserFirebase> user)
        {
            UserFirebase u = user.Object;
            u.ID = user.Key;
            return u;
        }
        public Request Request(FirebaseObject<Request> request)
        {
            Request r = request.Object;
            r.ID = request.Key;
            return r;
        }
    }
}
