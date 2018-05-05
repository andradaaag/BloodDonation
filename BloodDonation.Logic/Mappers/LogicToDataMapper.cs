using BloodDonation.Data.Models;
using BloodDonation.Business.Models;
using BloodDonation.Logic.Models;

namespace BloodDonation.Business.Mappers
{
    public class LogicToDataMapper
    {
        public FirebaseDonationForm MapDonationForm(DonationForm form)
        {
            return new FirebaseDonationForm();
        }

        public Hospital MapHospital(HospitalTransferObject hto)
        {
            return new Hospital(hto.Location, hto.Name);
        }
    }
}
