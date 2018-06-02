using BloodDonation.Utils.Enums;
using System.Collections.Generic;

namespace BloodDonation.Logic.Models
{
    public class BloodType
    {
        public string Group { get; set; }
        public bool RH { get; set; }
        public Component bloodComponent { get; set; }

        /**
         * Determine if this blood type is a compatible donor to the receiver blood type.
         */
        public bool isCompatible(BloodType receiver)
        {
            return (receiver.RH || !RH) && (receiver.Group == "AB" || Group == "O" || receiver.Group == Group);
        }
    }
}