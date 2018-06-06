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
    public class DonationCenterRepository
    {
        private FirebaseClient firebaseClient = new FirebaseClient("https://blooddonation-bc0b9.firebaseio.com/");
        private FirebaseToObject FirebaseToObject = new FirebaseToObject();
        private const string CHILD = "donationcenters";

        public DonationCenterRepository()
        {
        }

        public List<DonationCenter> FindAll()
        {
            return firebaseClient
                    .Child(CHILD)
                    .OrderByKey()
                    .OnceAsync<DonationCenter>()
                    .Result
                    .AsEnumerable()
                    .Select(i => FirebaseToObject.DonationCenter(i))
                    .ToList();
        }

        public void Save(DonationCenter newdc)
        {
            firebaseClient
                .Child(CHILD)
                .PostAsync(newdc);
        }

        public void Edit(DonationCenter dc)
        {
            firebaseClient
                .Child(CHILD)
                .Child(dc.ID)
                .PutAsync(dc);
        }

        public DonationCenter FindByName(String name)
        {
            return firebaseClient
                    .Child(CHILD)
                    .OrderBy("name")
                    .EqualTo(name)
                    .OnceAsync<DonationCenter>()
                    .Result
                    .AsEnumerable()
                    .Select(i => FirebaseToObject.DonationCenter(i))
                    .First();
        }

        public DonationCenter FindById(String Id)
        {
            if (Id == "none")
                return null;

            try
            {
                return firebaseClient
                        .Child(CHILD)
                        .OrderByKey()
                        .EqualTo(Id)
                        .OnceAsync<DonationCenter>()
                        .Result
                        .AsEnumerable()
                        .Select(i => FirebaseToObject.DonationCenter(i))
                        .First();
            }catch(Exception ex)
            {
                return null;
            }
        }

        public void DeleteById(string id)
        {
            firebaseClient
                .Child(CHILD)
                .Child(id)
                .DeleteAsync();
        }
    }
}
