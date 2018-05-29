using System.Collections.Generic;

namespace BloodDonation.Logic.Models
{
    public class BloodType
    {
        public string Group;
        public bool RH;
        
        /**
         * Determine if this blood type is a compatible donor to the receiver blood type.
         */
        public bool CanDonate(BloodType receiver)
        {
            if (receiver == null) return false;

            List<string> legend = new List<string>() { "O-", "O+", "A-", "A+", "B-", "B+", "AB-", "AB+" };

            // Compatibility array, compatibiliy[x][y] == 1  -->   y can donate to x
            int[][] compatibility = new int[8][] {
                new int[8] {1,0,0,0,0,0,0,0},
                new int[8] {1,1,0,0,0,0,0,0},
                new int[8] {1,0,1,0,0,0,0,0},
                new int[8] {1,1,1,1,0,0,0,0},
                new int[8] {1,0,0,0,1,0,0,0},
                new int[8] {1,1,0,0,1,1,0,0},
                new int[8] {1,0,1,0,1,0,1,0},
                new int[8] {1,1,1,1,1,1,1,1}
                };

            int donorIndex = legend.FindIndex(x => x == this.Group + ( (this.RH) ? "+" : "-") );
            int receiverIndex = legend.FindIndex(x => x == receiver.Group + ( (receiver.RH) ? "+" : "-") );

            return compatibility[receiverIndex][donorIndex] == 1;
        }
    }
}