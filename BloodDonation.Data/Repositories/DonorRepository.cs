using System;
using System.Collections.Generic;
using BloodDonation.Data.Models;

namespace BloodDonation.Data.Repositories
{
    public class DonorRepository
    {
        private List<Donation> myDonations;

        public DonorRepository()
        {
            myDonations = new List<Donation>();       
            Donation donation= new Donation();
            DonationCenter center = new DonationCenter("cluj", "test");
            center.location = "Cluj";
            donation.center = center;
            donation.donationDate = "11/2/2011";
            donation.quantity = 120;
            donation.testResult = "positive";
            myDonations.Add(donation);
            myDonations.Add(donation);
            myDonations.Add(donation);
        }

        public void AddDonationForm(FirebaseDonationForm form)
        {
            //add to firebase
        }

        public List<Donation> GetDonations()
        {
            return myDonations;
        }
    }
}