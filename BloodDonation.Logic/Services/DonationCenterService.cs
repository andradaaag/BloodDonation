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
            List<Data.Models.DonationCenter> donationCenters = donationCenterRepository.FindAll();
            List<DonationCenterTransferObject> donationCenterTransferObjects = new List<DonationCenterTransferObject>();

            foreach(Data.Models.DonationCenter dc in donationCenters)
            {
                donationCenterTransferObjects.Add(dataToLogicMapper.MapDonationCenterTransferObject(dc));
            }

            return donationCenterTransferObjects;
        }

        public void AddNewDonationCenter(DonationCenterTransferObject dcto)
        {
            donationCenterRepository.Save(logicToDataMapper.MapDonationCenter(dcto));
        }

        public List<DonationCenterTransferObject> FilterDonationCentersByNameAndLocation(string nameQuery, string locationQuery)
        {
            List<Data.Models.DonationCenter> donationCenters = donationCenterRepository.FindAll();
            List<DonationCenterTransferObject> donationCenterTransferObjects = new List<DonationCenterTransferObject>();

            foreach(Data.Models.DonationCenter donationCenter in donationCenters)
            {
                if (nameQuery != null && locationQuery != null)
                {
                    if (donationCenter.name.Contains(nameQuery) && donationCenter.location.Contains(locationQuery))
                        donationCenterTransferObjects.Add(dataToLogicMapper.MapDonationCenterTransferObject(donationCenter));
                }
                else if (nameQuery != null)
                {
                    if (donationCenter.name.Contains(nameQuery))
                        donationCenterTransferObjects.Add(dataToLogicMapper.MapDonationCenterTransferObject(donationCenter));
                }
                else
                {
                    if (donationCenter.location.Contains(locationQuery))
                        donationCenterTransferObjects.Add(dataToLogicMapper.MapDonationCenterTransferObject(donationCenter));
                }
            }

            return donationCenterTransferObjects;

        }

        public DonationCenterTransferObject GetDonationCenterById(String id)
        {
            Data.Models.DonationCenter d = donationCenterRepository.FindById(id);

            if (d == null)
                return null;

            return dataToLogicMapper
                        .MapDonationCenterTransferObject(d);
        }

        public void RemoveById(string id)
        {
            donationCenterRepository.DeleteById(id);
        }

    }
}
