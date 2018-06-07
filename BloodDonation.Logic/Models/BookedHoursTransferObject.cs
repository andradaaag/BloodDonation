using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Models
{
    public class BookedHoursTransferObject
    {
        public String bookingHour { get; set; }
        public String bookingDate { get; set; }
        public String center { get; set; }
        public String bookingId { get; set; }
    }
}
