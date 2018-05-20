using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Mappers
{
    public class PresentationToBusinessMapperPersonnel
    {

        public Logic.Models.Donation Donation(DonationModel donation)
        {
            return new Logic.Models.Donation
            {
                ID = donation.ID,
                Alt = donation.Alt,
                BloodType = new Logic.Models.BloodType
                {
                    Group = donation.BloodTypeGroup,
                    PH = donation.BloodTypePH == "Positive"
                },
                DonorId = donation.DonorId,
                HepatitisB = donation.HepatitisB,
                HepatitisC = donation.HepatitisC,
                Hiv = donation.Hiv,
                Htlv = donation.Htlv,
                Plasma = donation.Plasma,
                Quantity = donation.Quantity,
                RBC = donation.RBC,
                Stage = (Data.Models.Stage)Enum.Parse(typeof(Data.Models.Stage), donation.Stage),
                Syphilis = donation.Syphilis,
                Thrombocytes = donation.Thrombocytes,
                DonationTime = donation.DonationTime
            };
        }
        public Logic.Models.StoredBlood StoredBlood(StoredBloodModel b)
        {
            return new Logic.Models.StoredBlood
            {
                BloodType = new Logic.Models.BloodType
                {
                    Group = b.BloodTypeGroup,
                    PH = b.BloodTypePH == "Positive"
                },
                Component = (Data.Models.Component)Enum.Parse(typeof(Data.Models.Component), b.Component),
                CollectionDate = b.CollectionDate,
                Amount = b.Amount,
                ID = b.ID
            };
        }

        public Logic.Models.Personnel Personnel(Personnel p)
        {
            return new Logic.Models.Personnel
            {
                ID = p.ID,
                isApproved = p.isApproved,
                firstName = p.firstName,
                lastName = p.lastName,
                emailAddress = p.emailAddress,
                DOB = p.DOB,
                Address = p.Address,
                CityTown = p.CityTown,
                Country = p.Country,
                Residence = p.Residence,
                ResCityTown = p.ResCityTown,
                ResCountry = p.ResCountry
            };
        }

        public Logic.Models.Status Status(Status s)
        {
            return (Logic.Models.Status)s;
        }
    }
}