using BloodDonation.Logic.Models;
using BloodDonation.Logic.Services;
using BloodDonation.Mappers;
using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using RequestComponent = BloodDonation.Models.RequestComponent;

namespace BloodDonation.Services
{
    public class DoctorServicePresentation
    {
        private readonly DoctorService doctorService = new DoctorService();

        private readonly Logic.Services.RequestService requestService =
            new Logic.Services.RequestService();

        private readonly Logic.Services.DonationService donationService =
            new Logic.Services.DonationService();


        private readonly PresentationToBusinessMapper
            _presentationToBusinessMapper = new PresentationToBusinessMapper();

        private readonly BusinessToPresentationMapper
            _businessToPresentationMapper = new BusinessToPresentationMapper();

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
            List<DoctorTransferObject> doctorTransferObjects =
                doctorService.FilterDoctorsBySearchQuery(model.SearchQuery.Replace(" ", ""));

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

        public List<DonationStatusModel> GetDonationStatusesForDoctor(string id)
        {
            var requests = requestService.FindByDoctorId(id);
            var donations = donationService.FindAll();
            var result = new List<DonationStatusModel>();

            foreach (var request in requests)
            {
                var aux = new DonationStatusModel
                {
                    RecipientCNP = request.patientCnp,
                    BloodTypeRH = request.bloodType.RH ? "Positive" : "Negative",
                    BloodTypeGroup = request.bloodType.Group,
                    Component = (RequestComponent) request.bloodType.bloodComponent,
                    RequestedQuantity = request.quantity
                };

                var sum = 0;
                foreach (var donation in donations)
                {
                    if (donation.PatientCnp != null && donation.PatientCnp == request.patientCnp)
                        sum = sum + donation.Quantity;
                }

                aux.DonatedQuantity = sum;

                result.Add(aux);
            }

            return result;
        }
    }
}