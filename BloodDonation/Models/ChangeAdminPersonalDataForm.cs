using System;
using System.ComponentModel.DataAnnotations;

namespace BloodDonation.Models
{
    public class ChangeAdminPersonalDataForm
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public String Address { get; set; }
    }
}