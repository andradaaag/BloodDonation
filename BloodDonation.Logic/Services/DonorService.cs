using BloodDonation.Data.Repositories;
using BloodDonation.Logic.Mappers;
using BloodDonation.Logic.Models;

namespace BloodDonation.Logic.Services
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
