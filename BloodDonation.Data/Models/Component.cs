using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Data.Models
{
    public enum Component
    {
        Thrombocytes,
        //  shelf time: 5 days
        RedBloodCells,
        //  shelf time: 42 days
        Plasma,
        //  shelf time: several months
        // TODO: find out a more exact time
        Whole
        

    }
}
