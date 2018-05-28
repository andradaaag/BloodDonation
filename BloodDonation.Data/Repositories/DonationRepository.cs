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
            try
            {
                return firebaseClient
                    .Child(CHILD)
                    .OrderBy("Stage")
                    .StartAt(0)
                    .EndAt(2)
                    .OnceAsync<Donation>()
                    .Result
                    .AsEnumerable()
                    .Select(i => FirebaseToObject.Donation(i))
                    .ToList();
            }
            catch(Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                Console.Out.WriteLine(ex.StackTrace);
                return new List<Donation>();
            }
        }

        public List<Donation> FindByDonationCenter(string donationCenterID)
        {
            try
            {
                return firebaseClient
                    .Child(CHILD)
                    .OrderBy("DonationCenterID")
                    .EqualTo(donationCenterID)
                    .OnceAsync<Donation>()
                    .Result
                    .AsEnumerable()
                    .Select(i => FirebaseToObject.Donation(i))
                    .ToList();
            }
            catch(Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                Console.Out.WriteLine(ex.StackTrace);
                return null;
            }
        }

         public List<Donation> FindByPatientCnp(string patientCnp)
        {
            try
            {
                return firebaseClient
                    .Child(CHILD)
                    .OrderBy("PatientCnp")
                    .EqualTo(patientCnp)
                    .OnceAsync<Donation>()
                    .Result
                    .AsEnumerable()
                    .Select(i => FirebaseToObject.Donation(i))
                    .ToList();
            }
            catch(Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                Console.Out.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public int FindDonatedQuantityForPatient(string patientCnp)
        {
            try
            {
                return firebaseClient
                    .Child(CHILD)
                    .OrderBy("PatientCnp")
                    .EqualTo(patientCnp)
                    .OnceAsync<Donation>()
                    .Result
                    .AsEnumerable()
                    .Select(i => FirebaseToObject.Donation(i).Quantity)
                    .Sum();
                    
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                Console.Out.WriteLine(ex.StackTrace);
                return -1;
            }
        }

        public List<Donation> FindAll()
        {
            try
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
            catch(Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                Console.Out.WriteLine(ex.StackTrace);
                return new List<Donation>();
            }
        }

        public void Add(Donation d)
        {
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
            try
            {
                return FirebaseToObject.Donation(
                 firebaseClient
                 .Child(CHILD)
                 .OrderByKey()
                 .StartAt(id)
                 .LimitToFirst(1)
                 .OnceAsync<Donation>()
                 .Result
                 .First());
            } catch(Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                Console.Out.WriteLine(ex.StackTrace);
                return null;
            }
        }
    }
}
