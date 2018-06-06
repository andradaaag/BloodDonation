using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class ShowBookingDate
    {
        public List<BookedDates> donorBookedDates { get; set; }

        public List<BookedDates> getAllDates()
        {
            return donorBookedDates;
        }

        public void AddDate (BookedDates date)
        {
            donorBookedDates.Add(date);
        }
    }
}