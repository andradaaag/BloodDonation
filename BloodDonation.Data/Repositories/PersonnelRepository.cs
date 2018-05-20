using BloodDonation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using Firebase.Database.Query;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Data.Mapper;
using System.IO;

namespace BloodDonation.Data.Repositories
{
    public class PersonnelRepository
    {
        //TODO - here it will be linked with firebase, rn just a mock repo

        private FirebaseClient firebaseClient = new FirebaseClient("https://blooddonation-bc0b9.firebaseio.com/");
        private FirebaseToObject FirebaseToObject = new FirebaseToObject();
        // TODO: REDO CODE TO WORK WITH DATABASE
        private List<Personnel> tempList = new List<Personnel>();
        private string filePath = "C:\\Users\\Razvan\\Desktop\\ISS\\BloodDonation\\data.txt";

        public PersonnelRepository()
        {
            // TODO: REDO CODE TO WORK WITH DATABASE
            loadFromFile();

        }

        ~PersonnelRepository()
        {
            saveToFile();
        }

        public void loadFromFile()
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                String line = "";
                while(true)
                {
                    line = sr.ReadLine();
                    if (line == null)
                        break;
                    List<string> args = line.Split(',').ToList();
                    tempList.Add(new Personnel
                    {
                        ID = args[0],
                        firstName = args[1],
                        lastName = args[2],
                        emailAddress = args[3],
                        DOB = DateTime.Today,
                        Address = args[4],
                        CityTown = args[5],
                        Country = args[6],
                        isApproved = true
                    });
                }
            }
        }

        public void saveToFile()
        {
            File.WriteAllText(filePath, String.Empty);
            using (StreamWriter sw = new StreamWriter(filePath,true))
            {
                foreach(Personnel p in tempList)
                {
                    sw.WriteLine(p.ID + "," + p.firstName + "," + p.lastName + "," + p.emailAddress + "," + p.Address + "," +
                        p.CityTown + "," + p.Country);
                }
            }
        }

        public List<Personnel> FindAll()
        {
            // TODO: REDO CODE TO WORK WITH DATABASE
            return tempList;   
        }
        public void Add(Personnel p)
        {
            // TODO: REDO CODE TO WORK WITH DATABASE
            tempList.Add(p);
            saveToFile();
        }
        public void Edit(Personnel p)
        {
            // TODO: REDO CODE TO WORK WITH DATABASE
            var index = tempList.FindIndex(x => x.ID == p.ID);
            tempList.RemoveAt(index);
            tempList.Insert(index, p);
            saveToFile();
        }
        public Personnel GetOne(string id)
        {
            // TODO: REDO CODE TO WORK WITH DATABASE
            var index = tempList.FindIndex(x => x.ID == id);
            return tempList.ElementAt(index);
        }
    }
}
