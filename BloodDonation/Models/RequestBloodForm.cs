﻿using BloodDonation.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class RequestBloodForm
    {
        public String patientCnp { get; set; }
        public String bloodRh { get; set; }
        public String bloodGroup { get; set; }
        public int quantity { get; set; }
        public Component bloodComponent { get; set; }
        public DoctorUrgencyLevel urgency { get; set; }
    }
}