using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Utils.Enums
{
    public enum PatientStatus
    {
        AwaitingBlood = 0,
        EnoughBlood = 1,
        Finished = 2,
        Dead = -1,
        AliveAndWell = 3
    }
}
