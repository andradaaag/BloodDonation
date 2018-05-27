using BloodDonation.Logic.Mappers;
using BloodDonation.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Logic.Models;

namespace BloodDonation.Logic.Services
{
    public class RequestService
    {

        private RequestRepository Repository = new RequestRepository();
        private LogicToDataMapperPersonnel LogicToData = new LogicToDataMapperPersonnel();
        private DataToLogicMapperPersonnel DataToLogic = new DataToLogicMapperPersonnel();

        public List<RequestPersonnel> FindAll()
        {
            return Repository
                .FindAll()
                .AsEnumerable()
                .Select(i => DataToLogic.Request(i))
                .ToList();
        }

        public void EditStatus(string id,Status s)
        {
            Repository
                .EditStatus(id, LogicToData.Status(s));
        }

        public RequestPersonnel GetOne(string id)
        {
            return DataToLogic.Request(Repository.GetOne(id));
        }

        public List<RequestPersonnel> FindByDoctorId(string id)
        {
            return Repository
                   .GetRentalByDoctorId(id)
                   .AsEnumerable()
                   .Select(i => DataToLogic.Request(i))
                   .ToList();
        }

        public void AddRequest(RequestPersonnel request)
        {
            Repository.Save(LogicToData.RequestPersonelToRequest(request));
        }
    }
}
