using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
   

    public class TransferObjectForAcceptView
    {
        public RequestPersonnel bloodRequest { get; set; }
        public List<StoredBloodModel> usedBlood { get; set; }

       

    }
}