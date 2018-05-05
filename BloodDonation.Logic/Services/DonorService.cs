using System.Collections.Generic;
using BloodDonation.Data.Repositories;
using BloodDonation.Business.Mappers;
using BloodDonation.Business.Models;
using BloodDonation.Data.Models;
using BloodDonation.Logic.Mappers;
using BloodDonation.Logic.Models;

namespace BloodDonation.Business.Services
{
    public class DonorService
    {
        private readonly DonorRepository donorRepository = new DonorRepository();
        private readonly LogicToDataMapper logicToDataMapper = new LogicToDataMapper();
        private readonly DataToLogicMapper dataToLogicMapper = new DataToLogicMapper();


        public void AddDonationForm(DonationForm form)
        {
            var firebaseDonationForm = logicToDataMapper.MapDonationForm(form);
            donorRepository.AddDonationForm(firebaseDonationForm);
        }

        public List<DonationDetails> GetDonationDetails()
        {
            List<Donation> myDonations = donorRepository.GetDonations();
            List<DonationDetails> donationDetails = new List<DonationDetails>();
            foreach (Donation donation in myDonations)
            {
                donationDetails.Add(dataToLogicMapper.MapDonationToDonationDetails(donation));
            }

            return donationDetails;
        }
    }
}