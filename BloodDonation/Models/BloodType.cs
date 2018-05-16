using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class BloodType
    {
        [Required(ErrorMessage = "Blood group must be selected")]
        public string Group { get; set; }
        [Required(ErrorMessage = "PH must be selected")]
        public string PH { get; set; }

    }
}