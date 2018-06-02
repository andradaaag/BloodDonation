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
    public class DonorRepository
    {
        private FirebaseClient firebaseClient = new FirebaseClient("https://blooddonation-bc0b9.firebaseio.com/");
        private FirebaseToObject FirebaseToObject = new FirebaseToObject();
        private const string CHILD = "donors";


        private List<Donation> myDonations;

        public DonorRepository()
        {
            myDonations = new List<Donation>();

            Donation donation = new Donation();
            DonationCenter center = new DonationCenter("cluj", "test");
            donation.center = center;
            donation.donationDate = "11/2/2011";
            donation.quantity = 120;
            donation.testResult = "positive";
            myDonations.Add(donation);
            myDonations.Add(donation);
            myDonations.Add(donation);

        }

        public bool IsIDPresent(string id)
        {
            return firebaseClient
                .Child(CHILD)
                .OrderByKey()
                .EqualTo(id)
                .OnceAsync<Donor>()
                .Result.AsEnumerable().Count() == 1;
        }

        public void Save(Donor newDonor)
        {
            firebaseClient
                .Child(CHILD)
                .Child(newDonor.ID)
                .PutAsync(newDonor);
        }

        public void UpdateDonorDetails(Donor donor)
        {
            Donor oldDonor = new Donor();
            foreach (Donor d in GetDonors())
            {
                if (d.ID == donor.ID)
                {
                    oldDonor = d;
                }
            }
            oldDonor.firstName = donor.firstName;
            oldDonor.lastName = donor.lastName;
            oldDonor.Country = donor.Country;
            oldDonor.Address = donor.Address;
            oldDonor.emailAddress = donor.emailAddress;
            oldDonor.additionalCommentaries = donor.additionalCommentaries;
        }

        public List<Donation> GetDonations()
        {
            return myDonations;
        }


        public List<Donor> GetDonors()
        {
            try
            {
                return firebaseClient
                   .Child(CHILD)
                   .OrderByKey()
                   .OnceAsync<Donor>()
                   .Result
                   .AsEnumerable()
                   .Select(i => FirebaseToObject.Donor(i))
                   .ToList();
            }
            catch (Exception ex)
            {
                return new List<Donor>();
            }
        }

        public Donor GetDonorByCNP(string CNP)
        {
            return FirebaseToObject.Donor(firebaseClient
                .Child(CHILD)
                .OrderBy("CNP")
                .EqualTo(CNP)
                .OnceAsync<Donor>()
                .Result
                .First());

        }
    }
}