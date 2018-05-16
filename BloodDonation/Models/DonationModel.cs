
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class DonationModel
    {

        [Required(ErrorMessage = "Blood type must be selected")]
        public BloodType BloodType { get; set; }

        //  TODO: find a way to get this in the interface
        public string DonorId { get; set; }

        //  will be chanced to an enum when passed forward
        public string Stage { get; set; }

        public int Quantity { get; set; }

        //they can variate from person to person, also different sexes have different amounts

        //(about 55-60%)
        public int Plasma { get; set; }

        //red blood cells (about 40-45%)
        public int RBC { get; set; }

        //couldn't find them, they seem to be under 1% tho
        public int Thrombocytes { get; set; }


        //  for who you want to donate
        public string Cnp { get; set; }

        //empoch time
        public long DonationTime { get; set; }

        //  reasons why the blood might not be accepted
        //  HIV/AIDS, hepatitis B, hepatitis C, syphilis, HTLV, ALT
        public bool Hiv { get; set; }
        public bool HepatitisB { get; set; }
        public bool HepatitisC { get; set; }
        public bool Syphilis { get; set; }
        public bool Htlv { get; set; }
        public bool Alt { get; set; }
    }
}