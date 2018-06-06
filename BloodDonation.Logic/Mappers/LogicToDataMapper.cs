using BloodDonation.Data.Models;
using BloodDonation.Business.Models;
using BloodDonation.Logic.Models;

namespace BloodDonation.Business.Mappers
{
    public class LogicToDataMapper
    {
        // TODO - modify here

        public Data.Models.Hospital MapHospital(HospitalTransferObject hto)
        {
            return new Data.Models.Hospital(hto.Location, hto.Name);
        }

        public Data.Models.DonationCenter MapDonationCenter(DonationCenterTransferObject dcto)
        {
            return new Data.Models.DonationCenter(dcto.Location, dcto.Name);
        }

        // end todo

        public Doctor MapNewDoctor(NewUserTransferObject nuto)
        {
            return new Doctor()
            {
                ID = nuto.UID,
                HospitalId = nuto.Hospital,
                firstName = nuto.FirstName,
                lastName = nuto.LastName,
                DOB = nuto.DOB,
                Address = nuto.Address,
                CityTown = nuto.City,
                Country = nuto.Country,
                Residence = nuto.OtherAddress,
                ResCityTown = nuto.OtherAddress,
                ResCountry = nuto.OtherCountry,
                emailAddress = nuto.Email,

                isReviewed = nuto.isReviewed,
                isValid = nuto.isValid

            
                //TODO - somehow add hospital id to a list of now nonexisting ids
            };
        }

        public Donor MapNewDonor(NewUserTransferObject nuto)
        {
            return new Donor()
            {
                ID = nuto.UID,
                firstName = nuto.FirstName,
                lastName = nuto.LastName,
                DOB = nuto.DOB,
                Address = nuto.Address,
                CityTown = nuto.City,
                Country = nuto.Country,
                Residence = nuto.OtherAddress,
                ResCityTown = nuto.OtherAddress,
                ResCountry = nuto.OtherCountry,
                emailAddress = nuto.Email,
                CNP = nuto.CNP
            };
        }

        public Data.Models.DonationCenterPersonnel MapNewDonationCenterPersonnel(NewUserTransferObject nuto)
        {
            return new Data.Models.DonationCenterPersonnel()
            {
                ID = nuto.UID,
                firstName = nuto.FirstName,
                lastName = nuto.LastName,
                DOB = nuto.DOB,
                Address = nuto.Address,
                CityTown = nuto.City,
                Country = nuto.Country,
                Residence = nuto.OtherAddress,
                ResCityTown = nuto.OtherAddress,
                ResCountry = nuto.OtherCountry,
                emailAddress = nuto.Email,

                isReviewed = nuto.isReviewed,
                isValid = nuto.isValid,
                DonationCenterID = nuto.DonationCenter
                //TODO - somehow add donation center id to a list of now nonexisting ids
            };
        }
    }
}
