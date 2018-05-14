using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Data.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace BloodDonation.Data.Repositories
{
    public class DoctorRepository
    {
        private List<Doctor> myDoctors;

        public DoctorRepository()
        {
            myDoctors = new List<Doctor>();
        }

        private async Task<List<Doctor>> GetData()
        {
            var firebaseClient = new FirebaseClient("https://blooddonation-bc0b9.firebaseio.com/");

            var docs = await firebaseClient.Child("doctors")
                .OrderByKey()
                .LimitToFirst(3)
                .OnceAsync<Doctor>();

            var l = new List<Doctor>();
            
            foreach (var d in docs)
            {
                l.Add(d.Object);
            }

            return l;
        }


        public async Task<List<Doctor>> FindAll()
        {
            return await GetData();
        }
    }
}