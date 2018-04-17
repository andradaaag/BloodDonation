using BloodDonation.Business.Models;
using BloodDonation.Models;

namespace BloodDonation.Mappers
{
    public class PresentationToBusinessMapper
    {
        public DonationForm MapDonationForm(SignUpForm form)
        {
            return new DonationForm()
            {
                Address = form.Address,
                City = form.City,
                Country = form.Country,
                DOB = form.DOB,
                FirstName = form.FirstName,
                LastName = form.LastName,
                OtherAddress = form.OtherAddress,
                OtherCity = form.OtherCity,
                OtherCountry = form.OtherCountry,
                UID = form.UID
            };
        }
    }
}