using BloodDonation.Data.Models;
using BloodDonation.Logic.Models;

namespace BloodDonation.Logic.Mappers
{
    public class DataToLogicMapperDonor
    {
        public DonationDetails MapDonationToDonationDetails(Data.Models.Donation donation)
        {
            return new DonationDetails()
            {
                ID = donation.ID,
                CenterLocation = donation.center.location,
                Quantity = donation.quantity,
                TestResult = donation.testResult,
                DonationDate = donation.donationDate
            };
        }

        public DonorDetailsTransferObject MapDonorDetailsTransferObject(Donor donor)
        {
            return new DonorDetailsTransferObject()
            {
                ID = donor.ID,
                FirstName = donor.firstName,
                LastName = donor.lastName,
                DateOfBirth = donor.DOB,
                Address = donor.Address,
                Email = donor.emailAddress,
                Country = donor.Country,
                Commentaries = donor.additionalCommentaries,

                Cnp = donor.CNP,
                Residence= donor.Residence,
                CityTown= donor.CityTown

            };
        }

        public BookedHoursTransferObject MapBooking(Data.Models.Booking b)
        {
            return new BookedHoursTransferObject()
            {
                bookingDate = b.Date,
                bookingHour = b.Hour,
                bookingId = b.ID,
                center = b.DonationCenterId
            };
        }
       
    }
}