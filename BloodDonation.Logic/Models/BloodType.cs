namespace BloodDonation.Logic.Models
{
    public class BloodType
    {
        public string Group { get; set; }
        public bool RH { get; set; }
        public BloodDonation.Data.Models.Component bloodComponent { get; set; }
    }
}