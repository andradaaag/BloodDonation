
using BloodDonation.Logic.Models;
using BloodDonation.Models;

namespace BloodDonation.Mappers
{
    public class PresentationToBusinessMapper
    {
        public NewUserTransferObject MapNewUserTransferObject(SignUpForm signUpForm)
        {
            return new NewUserTransferObject()
            {
                UID = signUpForm.UID,
                FirstName = signUpForm.FirstName,
                LastName = signUpForm.LastName,
                DOB = signUpForm.DOB,
                Address = signUpForm.Address,
                City = signUpForm.City,
                Country = signUpForm.Country,
                OtherAddress = signUpForm.OtherAddress,
                OtherCity = signUpForm.OtherCity,
                OtherCountry = signUpForm.OtherCountry,
                Email = signUpForm.Email,
                Hospital = signUpForm.Hospital,
                DonationCenter = signUpForm.DonationCenter,
                CNP= signUpForm.CNP
                
            };
        }
    }
}