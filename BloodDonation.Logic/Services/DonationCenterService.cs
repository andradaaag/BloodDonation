using BloodDonation.Business.Mappers;
using BloodDonation.Data.Models;
using BloodDonation.Data.Repositories;
using BloodDonation.Logic.Mappers;
using BloodDonation.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Services
{
    public class DonationCenterService
    {
        private readonly DonationCenterRepository donationCenterRepository = new DonationCenterRepository();
        private readonly LogicToDataMapper logicToDataMapper = new LogicToDataMapper();
        private readonly DataToLogicMapper dataToLogicMapper = new DataToLogicMapper();

        public List<DonationCenterTransferObject> GetAllDonationCenters()
        {
            List<DonationCenter> donationCenters = donationCenterRepository.findAll();
            List<DonationCenterTransferObject> donationCenterTransferObjects = new List<DonationCenterTransferObject>();

            foreach(DonationCenter dc in donationCenters)
            {
                donationCenterTransferObjects.Add(dataToLogicMapper.MapDonationCenterTransferObject(dc));
            }

            return donationCenterTransferObjects;
        }

    }
}
