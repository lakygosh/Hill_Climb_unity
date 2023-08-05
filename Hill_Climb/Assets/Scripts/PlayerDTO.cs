using System;
using System.Xml.Serialization;
using Car;

namespace DefaultNamespace
{
    
    [XmlRoot("Player")]
    public class PlayerDTO
    {
        [XmlElement("UserName")]
        public String Name { get; set; } = "";

        [XmlElement("UserSurname")]
        public String Surname{ get; set; } = "";
        [XmlElement("UserId")]
        public int Id{ get; set; }

        [XmlElement("Coins")] public float Coins { get; set; } = 0;
        
        [XmlElement("BestScore")] public float BestScore { get; set; } = 0;
        
        [XmlIgnore]
        public float LastScore { get; set; } = 0;


        public PlayerDTO(string name, string surname, int id, float coins, float bestScore)
        {
            Name = name;
            Surname = surname;
            Id = id;
            Coins = coins;
            BestScore = bestScore;
        }

        public PlayerDTO(string name, string surname, int id)
        {
            Name = name;
            Surname = surname;
            Id = id;
        }

        public PlayerDTO()
        {
        }
    }
}