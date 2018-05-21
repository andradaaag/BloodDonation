using BloodDonation.Data.Models;
using BloodDonation.Business.Models;
using BloodDonation.Logic.Models;

namespace BloodDonation.Business.Mappers
{
    public class LogicToDataMapper
    {
        

        public Data.Models.Hospital MapHospital(HospitalTransferObject hto)
        {
            return new Data.Models.Hospital(hto.Location, hto.Name);
        }

        public Data.Models.DonationCenter MapDonationCenter(DonationCenterTransferObject dcto)
        {
            return new Data.Models.DonationCenter(dcto.Location, dcto.Name);
        }
    }
}
