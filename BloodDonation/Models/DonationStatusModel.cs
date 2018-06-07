namespace BloodDonation.Models
{
    public class DonationStatusModel
    {
        public string RecipientCNP { get; set; }

        public int RequestedQuantity { get; set; }

        public int DonatedQuantity { get; set; }

        public string BloodTypeGroup { get; set; }

        public string BloodTypeRH { get; set; }

        public RequestComponent Component { get; set; }
    }
}