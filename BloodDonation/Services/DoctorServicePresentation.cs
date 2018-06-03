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
    public class DoctorServicePresentation
    {
        private readonly DoctorService doctorService = new DoctorService();
        private readonly PresentationToBusinessMapper _presentationToBusinessMapper = new PresentationToBusinessMapper();
        private readonly BusinessToPresentationMapper _businessToPresentationMapper = new BusinessToPresentationMapper();

        public ManageRequestsModel GetDoctorAccountRequests()
        {
            List<AccountRequest> doctorAccountRequests = doctorService.GetDoctorAccountRequests();
            ManageRequestsModel manageRequestsModel = new ManageRequestsModel();
            foreach (AccountRequest ar in doctorAccountRequests)
            {
                manageRequestsModel.AddDoctorAccountRequest(_businessToPresentationMapper.MapDoctorAccountRequest(ar));
            }

            return manageRequestsModel;
        }

        public ManageAccountsModel GetDoctorAccounts()
        {
            List<DoctorTransferObject> doctorTransferObjects = doctorService.GetValidDoctors();
            ManageAccountsModel manageAccountsModel = new ManageAccountsModel();
            foreach (DoctorTransferObject dto in doctorTransferObjects)
            {
                manageAccountsModel.AddDoctorAccount(_businessToPresentationMapper.MapDoctorDisplayData(dto));
            }

            return manageAccountsModel;
        }

        public ManageAccountsModel SearchDoctors(ManageAccountsModel model)
        {
            List<DoctorTransferObject> doctorTransferObjects = doctorService.FilterDoctorsBySearchQuery(model.SearchQuery.Replace(" ", ""));

            model.ResetDoctorAccounts();
            model.IsViewingSearchResults = true;

            foreach (DoctorTransferObject dto in doctorTransferObjects)
            {
                model.AddDoctorAccount(_businessToPresentationMapper.MapDoctorDisplayData(dto));
            }

            return model;
        }

        public void ApproveAccount(string id)
        {
            doctorService.ApproveAccount(id);
        }

        public void DeleteAccount(string id)
        {
            doctorService.DeleteAccount(id);
        }
    }
}