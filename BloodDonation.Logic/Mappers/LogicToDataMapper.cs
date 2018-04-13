using BloodDonation.Data.Models;
using BloodDonation.Logic.Models;

namespace BloodDonation.Logic.Mappers
{
    public class LogicToDataMapper
    {
        public FirebaseDonationForm MapDonationForm(DonationForm form)
        {
            return new FirebaseDonationForm();
        }
    }
}
