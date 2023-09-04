using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Car;
using Newtonsoft.Json;

namespace DefaultNamespace
{
    
    [XmlRoot("Player")]
    [System.Serializable]
    [JsonObject("Player")]
    public class PlayerDTO
    {
        [XmlElement("UserName")]
        [JsonProperty ("UserName")]
        public String Name { get; set; } = "";

        [XmlElement("UserSurname")]
        [JsonProperty ("UserSurname")]
        public String Surname{ get; set; } = "";
        [XmlElement("UserId")]
        [JsonProperty ("UserId")]
        public int Id{ get; set; }

        [XmlElement("Coins")] 
        [JsonProperty ("Coins")]
        public float Coins { get; set; } = 0;
        
        [XmlElement("BestScore")]
        [JsonProperty ("BestScore")]
        public float BestScore { get; set; } = 0;
        
        [XmlElement("UnlockedCars")]
        [JsonProperty ("UnlockedCars")]
        public ArrayList UnlockedCars { get; set; } = new ArrayList();
        
        [XmlIgnore]
        [JsonIgnore]
        public float LastScore { get; set; } = 0;
        
        [XmlElement("SelectedCar")]
        [JsonProperty ("SelectedCar")]
        public string SelectedCar { get; set; } = "Car1";
        
        
        public PlayerDTO(string name, string surname, int id, float coins, float bestScore,
            ArrayList unlockedCars, string selectedCar)
        {
            Name = name;
            Surname = surname;
            Id = id;
            Coins = coins;
            BestScore = bestScore;
            UnlockedCars = unlockedCars;
            SelectedCar = selectedCar;
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