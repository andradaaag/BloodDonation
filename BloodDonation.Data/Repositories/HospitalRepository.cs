﻿using BloodDonation.Data.Mapper;
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
    public class HospitalRepository
    {

        private FirebaseClient firebaseClient = new FirebaseClient("https://blooddonation-bc0b9.firebaseio.com/");
        private FirebaseToObject FirebaseToObject = new FirebaseToObject();
        private const string CHILD = "hospitals";

        public HospitalRepository()
        {
            Hospital hospital = new Hospital("Cluj", "Bagdazar");
            this.Save(hospital);
            hospital = new Hospital("Cluj", "Regina Maria");
            this.Save(hospital);
            hospital = new Hospital("Bucuresti", "Urgente");
            this.Save(hospital);
            hospital = new Hospital("Sibiu", "Tulea");
            this.Save(hospital);
        }

        public List<Hospital> FindAll()
        {
            return firebaseClient
                    .Child(CHILD)
                    .OrderByKey()
                    .OnceAsync<Hospital>()
                    .Result
                    .AsEnumerable()
                    .Select(i => FirebaseToObject.Hospital(i))
                    .ToList();
        }

        public void Save(Hospital newhospital)
        {
            firebaseClient
                .Child(CHILD)
                .Child(newhospital.ID)
                .PutAsync(newhospital);
        }

        public void Edit(Hospital newhospital)
        {
            firebaseClient
                .Child(CHILD)
                .Child(newhospital.ID)
                .PutAsync(newhospital);
        }

        public List<Hospital> FindByName(String name)
        {
            return firebaseClient
                    .Child(CHILD)
                    .OrderBy("name")
                    .EqualTo(name)
                    .OnceAsync<Hospital>()
                    .Result
                    .AsEnumerable()
                    .Select(i => FirebaseToObject.Hospital(i))
                    .ToList();
        }


    }
}
