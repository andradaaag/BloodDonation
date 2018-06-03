using BloodDonation.Logic.Models;
using BloodDonation.Logic.Services;
using BloodDonation.Mappers;
using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Services
{
    public class DonationCenterServicePresentation
    {
        private readonly PresentationToBusinessMapper _presentationToBusinessMapper = new PresentationToBusinessMapper();
        private readonly BusinessToPresentationMapper _businessToPresentationMapper = new BusinessToPresentationMapper();
        private readonly DonationCenterService donationCenterService = new DonationCenterService();

        public ManageDonationCentersModel GetAllDonationCenters()
        {
            List<DonationCenterTransferObject> donationCenterTransferObjects = donationCenterService.GetAllDonationCenters();
            ManageDonationCentersModel manageDonationCentersModel = new ManageDonationCentersModel();
            foreach (DonationCenterTransferObject dcto in donationCenterTransferObjects)
            {
                manageDonationCentersModel.AddDonationCenter(_businessToPresentationMapper.MapDonationCenterDisplayData(dcto));
            }

            return manageDonationCentersModel;
        }

        public void AddNewDonationCenter(ManageDonationCentersModel model)
        {
            DonationCenterTransferObject newDC = new DonationCenterTransferObject
            {
                Location = model.Location,
                Name = model.Name
            };

            donationCenterService.AddNewDonationCenter(newDC);
        }

        public ManageDonationCentersModel SearchDonationCenters(ManageDonationCentersModel model)
        {
            string nameSearchQuery = model.SearchName;
            string locationSearchQuery = model.SearchLocation;

            List<DonationCenterTransferObject> donationCenterTransferObjects = donationCenterService.FilterDonationCentersByNameAndLocation(nameSearchQuery, locationSearchQuery);

            model.ResetDonationCenters();
            model.IsViewingSearchResults = true;

            foreach (DonationCenterTransferObject dcto in donationCenterTransferObjects)
            {
                model.AddDonationCenter(_businessToPresentationMapper.MapDonationCenterDisplayData(dcto));
            }

            return model;
        }

        public void RemoveById(string id)
        {
            donationCenterService.RemoveById(id);
        }

    }

}