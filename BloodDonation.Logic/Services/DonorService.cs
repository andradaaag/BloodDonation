using BloodDonation.Data.Repositories;
using BloodDonation.Business.Mappers;
using BloodDonation.Business.Models;

namespace BloodDonation.Business.Services
{
    public class DonorService
    {
        private readonly DonorRepository donorRepository = new DonorRepository();
        private readonly LogicToDataMapper logicToDataMapper = new LogicToDataMapper();

        public void AddDonationForm(DonationForm form)
        {
            var firebaseDonationForm = logicToDataMapper.MapDonationForm(form);
            donorRepository.AddDonationForm(firebaseDonationForm);
        }
    }
}
