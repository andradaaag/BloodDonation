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
        private readonly LogicToDataMapperDonor logicToDataDonor = new LogicToDataMapperDonor();
        private readonly DataToLogicMapperDonor dataToLogicDonor = new DataToLogicMapperDonor();

        private int DateTimeToUnix(DateTime dt)
        {
            return (int)(dt - new DateTime(1970, 1, 1)).TotalSeconds;
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

        public List<String> GetAvailableHours(String centerId, String date)
        {
            List<String> mylist = new List<string>();
            DateTime myDate = DateTime.ParseExact("07:00", "HH:mm",
                                       System.Globalization.CultureInfo.InvariantCulture);
            for (int i = 0; i < 7; i++)
            {
                DateTime d = myDate.AddHours(i);
                if (isAvailable(centerId, d.ToString("HH:mm"), date))
                {
                    mylist.Add(d.ToString("HH:mm"));
                }
            }
            return mylist;
        }

        private bool isAvailable(String centerId, String hour, String date)
        {
            List < String > hours= GetUnavailableHours(centerId, date);
            return !hours.Contains(hour);
        }

        public List<String> GetUnavailableHours(String centerId, String date)
        {
            return repo
                .FindByDate(date)
                .Where(i => i.DonationCenterId == centerId)
                .Select(i => dataToLogic.Booking(i).Hour)
                .ToList();

        }

        public void saveBooking(Booking newBooking)
        {
            newBooking.UnixTime = DateTimeToUnix(DateTime.Parse(newBooking.Date + " " + newBooking.Hour));
            repo.Save(logicToDataDonor.MapBooking(newBooking));
        }

        public List<BookedHoursTransferObject> GetDonorBookedHours(String donorId)
        {
            return repo.FindByDonorId(donorId)
                .Select(i => dataToLogicDonor.MapBooking(i))
                .ToList();
        }

    }
}
