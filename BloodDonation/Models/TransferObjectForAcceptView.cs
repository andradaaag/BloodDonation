using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
   

    public class TransferObjectForAcceptView
    {
        public RequestPersonnel bloodRequest { get; set; }
        public List<Logic.Models.StoredBlood> usedBlood { get; set; }

        public TransferObjectForAcceptView(List<Logic.Models.StoredBlood> usedBlood, RequestPersonnel bloodRequest)
        {
            this.usedBlood = usedBlood;
            this.bloodRequest = bloodRequest;

        }

    }
}