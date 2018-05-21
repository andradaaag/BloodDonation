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
                FirstName = donorForm.FirstName,
                LastName = donorForm.LastName,
                DateOfBirth = donorForm.DateOfBirth,
                Address = donorForm.Address,
                Email = donorForm.Email,
                Country = donorForm.Country,
                Commentaries = donorForm.Commentaries
            };
        }
    }
}