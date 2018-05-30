using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Models
{
    public class StoredBloodAmounts
    {
        public int[] Whole { get; set; }
        public int[] RBC { get; set; }
        public int[] Plasma { get; set; }
        public int[] Trombocytes { get; set; }
    }
}
