using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class BookingModel
    {
        public string ID { get; set; }
        public string DonationCenterId { get; set; }
        public string DonorName { get; set; }
        public string DonorId { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; }
        public DateTime FullDate { get; set; }
    }
}