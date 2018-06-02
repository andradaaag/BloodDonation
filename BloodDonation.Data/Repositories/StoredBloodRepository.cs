using BloodDonation.Data.Mapper;
using BloodDonation.Data.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BloodDonation.Utils.Enums;
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

        public void DeleteById(string id)
        {
            firebaseClient
                .Child(CHILD)
                .Child(id)
                .DeleteAsync();
        }

        public async Task<int> GetQuantityForBloodType(BloodType bloodType,Component component,string donCenter)
        {
            
            try
            {
                return firebaseClient
                        .Child(CHILD)
                        .OrderBy("DonationCenterID")
                        .EqualTo(donCenter)
                        .OnceAsync<StoredBlood>()
                        .Result
                        .Select(i => FirebaseToObject.StoredBlood(i))
                        .Where(el=>el.Component == component)
                        .Where(el =>    el.BloodType.RH     == bloodType.RH 
                                     && el.BloodType.Group  == bloodType.Group)
                        .Select(el => el.Amount)
                        .Sum();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        ///CRISTI LOG EXPIRED BLOOD phase 3 get the list of expired blood "directly" (not rly but w/ever) from the database
        public List<StoredBlood> GetExpiredBlood(int dWhole, int dPlasma, int dRBC, int dThr, int crtTime)
        {

            try
            {
                return firebaseClient
                        .Child(CHILD)
                        .OrderBy("CollectionDate")
                        .OnceAsync<StoredBlood>()
                        .Result
                        .Select(i => FirebaseToObject.StoredBlood(i))
                        .Where(el => el.Component == Component.Plasma && (crtTime - el.CollectionDate) > dPlasma        ||
                                     el.Component == Component.RedBloodCells && (crtTime - el.CollectionDate) > dRBC    ||
                                     el.Component == Component.Thrombocytes && (crtTime - el.CollectionDate) > dThr     ||
                                     el.Component == Component.Whole && (crtTime - el.CollectionDate) > dWhole
                                    )
                        .ToList();

            }
            catch (Exception ex)
            {
                return new List<StoredBlood>();
            }
        }



    }
}