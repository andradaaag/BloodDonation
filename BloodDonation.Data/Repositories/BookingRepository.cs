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
    public class BookingRepository
    {

        private FirebaseClient firebaseClient = new FirebaseClient("https://blooddonation-bc0b9.firebaseio.com/");
        private FirebaseToObject FirebaseToObject = new FirebaseToObject();
        private const string CHILD = "bookings";

        public BookingRepository()
        {
        }

        public List<Booking> FindAll()
        {

            return firebaseClient
                    .Child(CHILD)
                    .OrderByKey()
                    .OnceAsync<Booking>()
                    .Result
                    .Select(i => FirebaseToObject.Booking(i))
                    .ToList();

        }

        public void Save(Booking booking)
        {
            firebaseClient
                .Child(CHILD)
                .PostAsync(booking);
        }

        public void Edit(Booking booking)
        {
            firebaseClient
                .Child(CHILD)
                .Child(booking.ID)
                .PutAsync(booking);
        }



        public Booking FindById(String Id)
        {

            return firebaseClient
                .Child(CHILD)
                .OrderByKey()
                .EqualTo(Id)
                .OnceAsync<Booking>()
                .Result
                .AsEnumerable()
                .Select(i => FirebaseToObject.Booking(i))
                .First();

        }

        public void DeleteById(string id)
        {
            firebaseClient
                .Child(CHILD)
                .Child(id)
                .DeleteAsync();
        }

        public List<Booking> FindByDonationCenter(String id)
        {
            return firebaseClient
                .Child(CHILD)
                .OrderBy("DonationCenterId")
                .EqualTo(id)
                .OnceAsync<Booking>()
                .Result
                .Select(i => FirebaseToObject.Booking(i))
                .ToList();
        }

        public List<Booking> FindByDonorId(String id)
        {
            return firebaseClient
                .Child(CHILD)
                .OrderBy("DonorId")
                .EqualTo(id)
                .OnceAsync<Booking>()
                .Result
                .Select(i => FirebaseToObject.Booking(i))
                .ToList();
        }
        public List<Booking> FindByDate(String date)
        {
            return firebaseClient
                .Child(CHILD)
                .OrderBy("Date")
                .EqualTo(date)
                .OnceAsync<Booking>()
                .Result
                .Select(i => FirebaseToObject.Booking(i))
                .ToList();
        }
    }
}
