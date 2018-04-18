
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BloodDonation.Data.Models
{
    /**
     * 
     */
    public abstract class User : BaseEntity
    {

        /**
         * 
         */
        public User()
        {
        }

        public User(string firstName, string lastName, string emailAddress, DateTime dOB, string address, string cityTown, string country)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.emailAddress = emailAddress;
            DOB = dOB;
            Address = address;
            CityTown = cityTown;
            Country = country;
        }



        /**
         * 
         */
        public string firstName;

        /**
         * 
         */
        public string lastName;

        /**
         * 
         */
        public string emailAddress;

        /**
         * 
         */
        public DateTime DOB;

        /**
         * 
         */
        public string Address;

        /**
         * 
         */
        public string CityTown;

        /**
         * 
         */
        public string Country;

        /**
         * 
         */
        public string Residence;

        /**
         * 
         */
        public string ResCityTown;

        /**
         * 
         */
        public string ResCountry;

    }
}