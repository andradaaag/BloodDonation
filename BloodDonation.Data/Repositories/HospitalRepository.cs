using BloodDonation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Data.Repositories
{
    public class HospitalRepository
    {
        private List<Hospital> hospitals;

        public HospitalRepository()
        {
            this.hospitals = new List<Hospital>();

            Hospital hospital = new Hospital("Cluj", "Bagdazar");
            hospital.ID = "1";
            this.hospitals.Add(hospital);

            hospital = new Hospital("Cluj", "Regina Maria");
            hospital.ID = "2";
            this.hospitals.Add(hospital);

            hospital = new Hospital("Bucuresti", "Urgente");
            hospital.ID = "3";
            this.hospitals.Add(hospital);

            hospital = new Hospital("Sibiu", "Tulea");
            hospital.ID = "4";
            this.hospitals.Add(hospital);
        }

        public List<Hospital> findAll()
        {
            return this.hospitals;
        }
    }
}
