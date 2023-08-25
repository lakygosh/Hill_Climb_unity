﻿using System;
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
        
        [XmlIgnore]
        [JsonIgnore]
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