using System;
using System.ComponentModel.DataAnnotations;

namespace BloodDonation.Models
{
    public class BaseEntity
    {
        public BaseEntity()
        {
        }

        public string ID { get; set; }
    }
}