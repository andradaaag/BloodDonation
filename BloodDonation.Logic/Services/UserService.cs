using BloodDonation.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Logic.Services
{
    public class UserService
    {
        private UserRepository Repository = new UserRepository();

        public string GetRole(string UID)
        {
            return Repository.GetRole(UID);
        }
    }
}
