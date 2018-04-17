using BloodDonation.Data.Models;
using BloodDonation.Business.Models;

namespace BloodDonation.Business.Mappers
{
    public class LogicToDataMapper
    {
        public FirebaseDonationForm MapDonationForm(DonationForm form)
        {
            return new FirebaseDonationForm();
        }
    }
}
