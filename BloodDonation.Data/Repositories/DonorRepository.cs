using System;
using System.Collections.Generic;
using BloodDonation.Data.Models;

namespace BloodDonation.Data.Repositories
{
    public class DonorRepository
    {
        private List<Donation> myDonations;
        private List<Donor> donorList;

        public DonorRepository()
        {
            myDonations = new List<Donation>();
            donorList= new List<Donor>();
            Donation donation = new Donation();
            DonationCenter center = new DonationCenter("cluj", "test");
            donation.center = center;
            donation.donationDate = "11/2/2011";
            donation.quantity = 120;
            donation.testResult = "positive";
            myDonations.Add(donation);
            myDonations.Add(donation);
            myDonations.Add(donation);

            Donor donor = new Donor("Decebal", "Popescu", "gica@yahoo.com", DateTime.Now, "No adderess ", "Capalna",
                "Romania", new DonationFormEntity(90), "No comments");
            donor.ID = "id";
            donorList.Add(donor);
        }

        public void AddDonationForm(FirebaseDonationForm form)
        {
            //add to firebase
        }

        public List<Donation> GetDonations()
        {
            return myDonations;
        }

        public List<Donor> GetDonors()
        {
            return donorList;
        }
    }
}