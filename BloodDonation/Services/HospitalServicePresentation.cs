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
    public class HospitalServicePresentation
    {
        private readonly HospitalService hospitalService = new HospitalService();
        private readonly PresentationToBusinessMapper _presentationToBusinessMapper = new PresentationToBusinessMapper();
        private readonly BusinessToPresentationMapper _businessToPresentationMapper = new BusinessToPresentationMapper();
        
        public ManageHospitalsModel GetAllHospitals()
        {
            List<HospitalTransferObject> hospitalTransferObjects = hospitalService.GetAllHospitals();
            ManageHospitalsModel manageHospitalsModel = new ManageHospitalsModel();
            foreach (HospitalTransferObject hto in hospitalTransferObjects)
            {
                manageHospitalsModel.AddHospital(_businessToPresentationMapper.MapHospitalDisplayData(hto));
            }

            return manageHospitalsModel;
        }

        public void AddNewHospital(ManageHospitalsModel model)
        {
            LocationService ls = new LocationService();

            List<Double> locations = ls.getCoordinates(model.Location);

            HospitalTransferObject newHospital = new HospitalTransferObject
            {
                Location = model.Location,
                Name = model.Name,
                Lat = locations[0],
                Lon = locations[1]
            };

            hospitalService.AddNewHospital(newHospital);
        }


        public ManageHospitalsModel SearchHosptials(ManageHospitalsModel model)
        {
            string nameSearchQuery = model.SearchName;
            string locationSearchQuery = model.SearchLocation;

            List<HospitalTransferObject> hospitalTransferObjects = hospitalService.FilterHospitalsByNameAndLocation(nameSearchQuery, locationSearchQuery);

            model.ResetHospitals();
            model.IsViewingSearchResults = true;

            foreach (HospitalTransferObject hto in hospitalTransferObjects)
            {
                model.AddHospital(_businessToPresentationMapper.MapHospitalDisplayData(hto));
            }
            return model;
        }

        public void RemoveById(string id)
        {
            hospitalService.RemoveById(id);
        }
    }
}