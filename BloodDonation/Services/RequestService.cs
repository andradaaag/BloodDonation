using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Services
{
    public class RequestService
    {
        private Mappers.BusinessToPresentationMapperPersonnel businessToPresentation = new Mappers.BusinessToPresentationMapperPersonnel();

        private readonly Logic.Services.RequestService reqService = new Logic.Services.RequestService();
        public TransferObjectForAcceptView AcceptRequest(string reqId, string UID)
        {
            Logic.Models.RequestPersonnel req = reqService.GetOne(reqId);
            List<StoredBloodModel> blood = reqService
                .AcceptRequest(req, UID)
                .Select(i => businessToPresentation.StoredBlood(i))
                .ToList();
            int diff = req.quantity - blood.Sum(i => i.Amount);
            if (diff > 0)
            {
                throw new Exception(diff.ToString());
            }
            return new TransferObjectForAcceptView
            {
                bloodRequest = businessToPresentation.Request(req),
                usedBlood = blood

            };
        }
    }
}