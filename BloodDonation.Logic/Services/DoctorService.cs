using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Data.Repositories;
using BloodDonation.Business.Mappers;
using BloodDonation.Business.Models;

namespace BloodDonation.Logic.Services
{
    class DoctorService
    {
        private readonly DoctorRepository doctorRepository = new DoctorRepository();
        private readonly LogicToDataMapper logicToDataMapper = new LogicToDataMapper();


    }
}
