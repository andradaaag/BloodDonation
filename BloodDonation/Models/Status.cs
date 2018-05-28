using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Models
{
    public enum Status
    {
        [Description("Being processed")]
        BeingProcessed,
        [Description("Accepted")]
        Accepted,
        [Description("On the way")]
        OnTheWay,
        [Description("Denied")]
        Denied,
        [Description("Completed")]
        Completed
    }
}
