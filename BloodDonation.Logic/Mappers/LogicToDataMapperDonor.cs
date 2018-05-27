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
                DonationFormEntity = new DonationFormEntity(detailsTransferObject.Weight),
                emailAddress = detailsTransferObject.Email,
                Country = detailsTransferObject.Country,
                additionalCommentaries = detailsTransferObject.Commentaries,
                cnp = detailsTransferObject.Cnp
            };
        }

        public FirebaseDonationForm MapDonationForm(DonationForm form)
        {
            return new FirebaseDonationForm();
        }
    }
}