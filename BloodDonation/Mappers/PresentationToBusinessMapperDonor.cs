using BloodDonation.Logic.Models;
using BloodDonation.Models;

namespace BloodDonation.Mappers
{
    public class PresentationToBusinessMapperDonor
    {
        public DonorDetailsTransferObject MapDonorForm(DonorAccountRequest donorForm)
        {
            return new DonorDetailsTransferObject()
            {
                ID = donorForm.ID,
                Cnp = donorForm.Cnp,
                FirstName = donorForm.FirstName,
                LastName = donorForm.LastName,
                DateOfBirth = donorForm.DateOfBirth,
                Address = donorForm.Address,
                Email = donorForm.Email,
                Country = donorForm.Country,
                Commentaries = donorForm.Commentaries,
                Residence = donorForm.Residence,
                ResCityTown = donorForm.ResCityTown,
                CityTown = donorForm.CityTown,
                ResCountry = donorForm.ResCountry
            };
        }

        public DonorSelectedDate MapSelectedDate(AvailableHoursModel model)
        {
            return new DonorSelectedDate()
            {
                donationCenter = model.donationCenter,
                bookingDate = model.bookingDate
            };
        }
    } 
}