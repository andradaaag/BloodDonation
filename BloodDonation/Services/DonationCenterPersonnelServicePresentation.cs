using BloodDonation.Logic.Models;
using BloodDonation.Logic.Services;
using BloodDonation.Mappers;
using BloodDonation.Models;
using System.Collections.Generic;

namespace BloodDonation.Services
{
    public class DonationCenterPersonnelServicePresentation
    {

        private readonly PresentationToBusinessMapper _presentationToBusinessMapper = new PresentationToBusinessMapper();
        private readonly BusinessToPresentationMapper _businessToPresentationMapper = new BusinessToPresentationMapper();
        private readonly DonationCenterPersonnelService donationCenterPersonnelService = new DonationCenterPersonnelService();

        public ManageRequestsModel GetDonationCenterPersonnelAccountRequests()
        {
            List<AccountRequest> donationCenterPersonnelAccountRequests = donationCenterPersonnelService.GetDonationCenterPersonnelAccountRequests();
            ManageRequestsModel manageRequestsModel = new ManageRequestsModel();
            foreach (AccountRequest ar in donationCenterPersonnelAccountRequests)
            {
                manageRequestsModel.AddDonationCenterPersonnelAccountRequest(_businessToPresentationMapper.MapDonationCenterPersonnelAccountRequest(ar));
            }

            return manageRequestsModel;
        }

        public ManageAccountsModel GetDonationCenterPersonnelAccounts()
        {
            List<DonationCenterPersonnelTransferObject> donationCenterPersonnelTransferObjects = donationCenterPersonnelService.GetValidDonationCenterPersonnel();
            ManageAccountsModel manageAccountsModel = new ManageAccountsModel();
            foreach (DonationCenterPersonnelTransferObject dcpto in donationCenterPersonnelTransferObjects)
            {
                manageAccountsModel.AddDonationCenterPersonnelAccount(_businessToPresentationMapper.MapDonationCenterPersonnelDisplayData(dcpto));
            }

            return manageAccountsModel;
        }

        public ManageAccountsModel SearchDonationCenterPersonnel(ManageAccountsModel model)
        {
            string searchQuery = model.SearchQuery;

            List<DonationCenterPersonnelTransferObject> donationCenterPersonnelTransferObjects = donationCenterPersonnelService.FilterDonationCenterPersonnelBySearchQuery(searchQuery.Replace(" ", ""));

            model.ResetDonationCenterPersonnelAccounts();
            model.IsViewingSearchResults = true;

            foreach (DonationCenterPersonnelTransferObject dcpto in donationCenterPersonnelTransferObjects)
            {
                model.AddDonationCenterPersonnelAccount(_businessToPresentationMapper.MapDonationCenterPersonnelDisplayData(dcpto));
            }

            return model;
        }

        public void ApproveAccount(string id)
        {
            donationCenterPersonnelService.ApproveAccount(id);
        }

        public void DeleteAccount(string id)
        {
            donationCenterPersonnelService.DeleteAccount(id);
        }


    }
}