using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Data.Repositories;
using BloodDonation.Business.Mappers;
using BloodDonation.Business.Models;
using BloodDonation.Logic.Mappers;
using BloodDonation.Data.Models;
using BloodDonation.Logic.Models;

namespace BloodDonation.Logic.Services
{
    public class DonationCenterPersonnelService
    {
        private readonly DonationCenterPersonnelRepository personnelRepository = new DonationCenterPersonnelRepository();
        private readonly LogicToDataMapper logicToDataMapper = new LogicToDataMapper();
        private readonly DataToLogicMapper dataToLogicMapper = new DataToLogicMapper();

        public List<AccountRequest> GetDCPersonnelAccountRequests()
        {
            List<DonationCenterPersonnel> myPersonnel = personnelRepository.findAll();
            List<AccountRequest> myPersonnelAccountRequests = new List<AccountRequest>();
            foreach(DonationCenterPersonnel personnel in myPersonnel)
            {
                myPersonnelAccountRequests.Add(dataToLogicMapper.MapDCPersonnelToAccountRequest(personnel));
            }
            return myPersonnelAccountRequests;
        }


    }

}
