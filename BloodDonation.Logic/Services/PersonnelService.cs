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

        public List<Personnel> FindAll()
        {
            List<Personnel> toReturn = new List<Personnel>();
            foreach(Data.Models.Personnel p in Repository.FindAll())
            {
                toReturn.Add(dataToLogicMapper.Personnel(p));
            }
            return toReturn;
        }

        public void Add(Personnel p)
        {
            Repository.Add(logicToDataMapper.Personnel(p));

        }

        public void Edit(Personnel p)
        {
            Repository.Edit(logicToDataMapper.Personnel(p));
        }

        public Personnel GetOne(string id)
        {
            return dataToLogicMapper.Personnel(  Repository.GetOne(id)  );
        }
    }
}
