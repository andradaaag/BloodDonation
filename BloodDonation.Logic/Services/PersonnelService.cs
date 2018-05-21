using BloodDonation.Business.Mappers;
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
    public class PersonnelService
    {
        private readonly PersonnelRepository Repository = new PersonnelRepository();
        private readonly LogicToDataMapperPersonnel logicToDataMapper = new LogicToDataMapperPersonnel();
        private readonly DataToLogicMapperPersonnel dataToLogicMapper = new DataToLogicMapperPersonnel();

        public List<AccountRequest> GetPersonnelAccountRequests()
        {
            // TO DO
            return null;
        }

        public List<DonationCenterPersonnel> FindAll()
        {
            List<DonationCenterPersonnel> toReturn = new List<DonationCenterPersonnel>();
            foreach(Data.Models.DonationCenterPersonnel p in Repository.FindAll())
            {
                toReturn.Add(dataToLogicMapper.Personnel(p));
            }
            return toReturn;
        }

        public void Add(DonationCenterPersonnel p)
        {
            Repository.Add(logicToDataMapper.Personnel(p));

        }

        public void Edit(DonationCenterPersonnel p)
        {
            Repository.Edit(logicToDataMapper.Personnel(p));
        }

        public DonationCenterPersonnel GetOne(string id)
        {
            return dataToLogicMapper.Personnel(  Repository.GetOne(id)  );
        }
    }
}
