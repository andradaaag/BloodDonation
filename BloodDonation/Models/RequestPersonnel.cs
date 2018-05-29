using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Models
{
    public class RequestPersonnel : BaseEntity
    {
        public RequestPersonnel() { }

        [JsonConverter(typeof(StringEnumConverter))]
        [Display(Name = "Status")]
        public Status status { get; set; } 

        public String destination;      //hospitalid
        public String source;           //donationcenterid
        public String doctorId;
        public String patientCnp;


        public String hospitalName;
        public String hospitalLocation;

        public String doctorName;

        public int quantity;
        public BloodType bloodType;
        public RequestComponent component { get; set; }
    }
}
