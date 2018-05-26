using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Models
{
    public class RequestList
    {
        public RequestList()
        {

        }

        public List<RequestPersonnelView> Requests { set; get; } = new List<RequestPersonnelView>();
    }
}
