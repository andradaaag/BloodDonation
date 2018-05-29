using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Data.Repositories;
using BloodDonation.Business.Mappers;
using BloodDonation.Business.Models;
using BloodDonation.Logic.Mappers;
using BloodDonation.Data.Models;
using BloodDonation.Logic.Models;

namespace BloodDonation.Models
{
    public class BloodType
    {
        public string Group { get; set; }
        public bool PH { get; set; }
        public Data.Models.Component component { get; set; }

    }
}
