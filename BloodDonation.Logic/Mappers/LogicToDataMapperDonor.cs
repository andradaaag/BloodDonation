using BloodDonation.Business.Models;
using BloodDonation.Data.Models;
using BloodDonation.Logic.Models;

namespace BloodDonation.Logic.Mappers
{
    public class LogicToDataMapperDonor
    {
        public Donor MapDonor(DonorDetailsTransferObject detailsTransferObject)
        {
            return new Donor()
            {
                ID = detailsTransferObject.ID,
                firstName = detailsTransferObject.FirstName,
                lastName = detailsTransferObject.LastName,
                DOB = detailsTransferObject.DateOfBirth,
                Address = detailsTransferObject.Address,
                emailAddress = detailsTransferObject.Email,
                Country = detailsTransferObject.Country,
                additionalCommentaries = detailsTransferObject.Commentaries,
                CNP = detailsTransferObject.Cnp,
                ResCityTown = detailsTransferObject.ResCityTown,
                Residence = detailsTransferObject.Residence,
                CityTown= detailsTransferObject.CityTown,
                ResCountry = detailsTransferObject.ResCountry
                                
            };
        }

        public FirebaseDonationForm MapDonationForm(DonationForm form)
        {
            return new FirebaseDonationForm();
        }

        public BloodDonation.Data.Models.Booking MapBooking(BloodDonation.Logic.Models.Booking newBooking)
        {
            return new BloodDonation.Data.Models.Booking()
            {
                Date = newBooking.Date,
                Hour = newBooking.Hour,
                DonationCenterId = newBooking.DonationCenterId,
                DonorId = newBooking.DonorId,
                DonorName = newBooking.DonorName,
                UnixTime = newBooking.UnixTime
            };
        }
    }
}