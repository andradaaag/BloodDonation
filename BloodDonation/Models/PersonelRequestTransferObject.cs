using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class PersonelRequestTransferObject
    {
        public List<RequestPersonnel> listRequests { get; set; }
        public Double LatDonationCenter { get; set; }
        public Double LonDonationCenter { get; set; }

    }
}