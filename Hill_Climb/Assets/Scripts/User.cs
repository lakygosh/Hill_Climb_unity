using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Pendulum_v3FullApp.Base
{
    [XmlRoot("User")]
    [XmlInclude(typeof(GameSesion))]
    public class User: IComparable
    {
        [XmlElement("UserName")]
        public string Name { get; set; } = "";
        [XmlElement("UserSurname")]
        public string Surname { get; set; } = "";
        [XmlElement("UserId")]
        public int Id { get; set; }
        [XmlElement("UserBirthYear")]
        public string BirthYear { get; set; }
        [XmlElement("UserDiagnosis")]
        public string Diagnosis { get; set; }
        [XmlElement("AcountDate")]
        public string AccountDate { get; set; }
        [XmlArray("GameSesionArray")]
        [XmlArrayItem("GameSesionObject")]
        public List<GameSesion> GameSesions = new List<GameSesion>();
        [XmlArray("AngleProgressArray")]
        [XmlArrayItem("AngleProgressObject")]
        public List<AngleProgress> AngleProgressArray = new List<AngleProgress>();

        private static List<int> idList;

        public User() { }

        public User(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            BirthYear = user.BirthYear;
            Diagnosis = user.Diagnosis;
            AccountDate = user.AccountDate;
        }
        public User(int generationMode)
        {
            Id = GenerateNewId();
            idList.Add(Id);
        }

        public User(string birthYear, string diagnosis, string accountDate)
        {
            Id = GenerateNewId();
            BirthYear = birthYear;
            Diagnosis = diagnosis;
            AccountDate = accountDate;
        }

        public static void InitIdList(List<User> uList)
        {
            idList = new List<int>();
            if (uList != null)
            {
                for (int i = 0; i < uList.Count; i++)
                {
                    idList.Add(uList[i].Id);
                }
            }

        }
        private int GenerateNewId()
        {
            Random ran = new Random();
            try
            {
                int ri = ran.Next(15000000);
                while (idList.Contains(ri))
                {
                    ri = ran.Next(15000000);
                }
                return ri;
            }
            catch (Exception)
            {
                return 0;
            }

        }
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            User otherUser = obj as User;
            if (otherUser != null)
            {
                return this.Surname.CompareTo(otherUser.Surname);
            }
            else
            {
                throw new ArgumentException("Object is not a User");
            }
        }
    }

    [XmlType("GameSesion")] // define Type
    public class GameSesion
    {
        [XmlElement("Training")]
        public string training { get; set; }
        [XmlElement("Date")]
        public string date { get; set; }
        [XmlElement("Duration")]
        public string duration { get; set; }
        [XmlElement("NumOfClicks")]
        public string numOfClicks { get; set; }
        [XmlElement("Game")]
        public string game { get; set; }

        public GameSesion() {
        }
        public GameSesion(string training, string date, string duration, string numOfClicks, string game)
        {
            this.training = training;
            this.date = date;
            this.duration = duration;
            this.numOfClicks = numOfClicks;
            this.game = game;
        }
    }
    [XmlType("AngleProgress")] // define Type
    public class AngleProgress
    {
        [XmlElement("Date")]
        public string date { get; set; }
        [XmlElement("AngleRange")]
        public string angleRange { get; set; }        

        public AngleProgress()
        {
        }
        public AngleProgress(string date, string angleRange)
        {
            this.date = date;
            this.angleRange = angleRange;            
        }
    }
}
