using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class BloodAmounts
    {
        public int[] Whole { get; set; }
        public int[] RBC { get; set; }
        public int[] Plasma { get; set; }
        public int[] Trombocytes { get; set; }
    }
}