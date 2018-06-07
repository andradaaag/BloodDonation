using BloodDonation.Logic.Models;
using BloodDonation.Models;
using System;

namespace BloodDonation.Mappers
{
    public class BusinessToPresentationMapperDonor
    {
        public DonorDonationDetails MapDonorDonationDetails(DonationDetails donationDetails)
        {
            return new DonorDonationDetails()
            {
                ID = donationDetails.ID,
                CenterLocation = donationDetails.CenterLocation,
                Quantity = donationDetails.Quantity,
                TestResult = donationDetails.TestResult,
                DonationDate = donationDetails.DonationDate
            };
        }

        public DonorAccountRequest MapDonorAccountRequest(DonorDetailsTransferObject donorAccountDetails)
        {

                return new DonorAccountRequest()
                {

                ID = donorAccountDetails.ID,
                Cnp = donorAccountDetails.Cnp,
                FirstName = donorAccountDetails.FirstName,
                LastName = donorAccountDetails.LastName,
                DateOfBirth = donorAccountDetails.DateOfBirth,
                Address = donorAccountDetails.Address,
                Email = donorAccountDetails.Email,
                Country = donorAccountDetails.Country,
                Commentaries = donorAccountDetails.Commentaries,
                Residence = donorAccountDetails.Residence,
                ResCityTown = donorAccountDetails.ResCityTown,
                ResCountry = donorAccountDetails.ResCountry,
                CityTown= donorAccountDetails.CityTown
                
            };

        }

        public BookedDates MapBookedDates( BookedHoursTransferObject bhto, String center)
        {
            return new BookedDates()
            {
                bookingDate = bhto.bookingDate,
                bookingHour = bhto.bookingHour,
                bookingId = bhto.bookingId,
                center = center
            };
        }
    }
}