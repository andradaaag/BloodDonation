using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Data.Models
{
    public class Booking
    {
        public string ID { get; set; }
        public string DonationCenterId { get; set; }
        public string DonorName { get; set; }
        public string DonorId { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; }
        public int UnixTime { get; set; }
    }
}
