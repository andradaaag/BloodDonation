using System.Collections.Generic;

namespace BloodDonation.Models
{
    public class ShowDonorDonations
    {
        private List<DonorDonationDetails> donorDonationDetails;

        public ShowDonorDonations()
        {
            this.donorDonationDetails = new List<DonorDonationDetails>();
        }

        public void AddDonationDetails(DonorDonationDetails details)
        {
            this.donorDonationDetails.Add(details);
        }

        public List<DonorDonationDetails> GetDonorDonationDetails()
        {
            return this.donorDonationDetails;
        }
    }
    
}