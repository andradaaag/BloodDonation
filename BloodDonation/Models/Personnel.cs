﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BloodDonation.Models
{
    public class Personnel: User
    {

        public bool isApproved { get; set; }

        public string DonationCenterID { get; set; }

        public Personnel() : base("N/A", "N/A", "N/A", DateTime.Now, "N/A", "N/A", "N/A")
        {
            isApproved = false;
        }

        public Personnel(string firstName, string lastName, string emailAddress, DateTime DOB, string address, string cityTown, string country)
            : base(firstName, lastName, emailAddress, DOB, address, cityTown, country)
        {
            isApproved = false;
        }
    }
}