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
    public class BookingService
    {
        private readonly BookingRepository repo = new BookingRepository();
        private readonly DataToLogicMapperPersonnel dataToLogic = new DataToLogicMapperPersonnel();
        private readonly DonationCenterPersonnelRepository persRepo = new DonationCenterPersonnelRepository();

        private int DateTimeToUnix(DateTime dt)
        {
            return dt.Subtract(new DateTime(1970, 1, 1)).Seconds;
        }

        public List<Booking> GetActiveBookings(string uid)
        {
            string donationCenterId = persRepo.GetOne(uid).DonationCenterID;
            DateTime today = DateTime.Now.Date + new TimeSpan(0, 0, 0);
            int unix = DateTimeToUnix(today);
            return repo
                .FindByDonationCenter(donationCenterId)
                .Select(i => dataToLogic.Booking(i))
                .Where(i => i.UnixTime >= unix)
                .OrderBy(i=>i.UnixTime)
                .ToList();
        }


    }
}
