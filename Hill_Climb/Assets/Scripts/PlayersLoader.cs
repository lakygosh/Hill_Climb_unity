using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using Car;
using DefaultNamespace;
using Pendulum_v3FullApp.Base;
using UnityEngine;

public class PlayersLoader : MonoBehaviour
{
    public static void LoadAllPlayers(ref List<PlayerDTO> playerDTOs)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        if (!path.EndsWith("\\")) path += "\\";
        path += "Pendulum_v3FullApp\\";
        XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerDTO>));

        try
        {
            if (File.Exists(path + "HCRPlayers.xml"))
            {
                StreamReader reader = new StreamReader(path + "HCRPlayers.xml");
                if (File.Exists(path + "HCRPlayers_Backup.xml"))
                {
                    File.Delete(path + "HCRPlayers_Backup.xml");
                }

                File.Copy(path + "HCRPlayers.xml", path + "HCRPlayers_Backup.xml");
                playerDTOs = (List<PlayerDTO>)serializer.Deserialize(reader);
                reader.Close();
            }
            else
            {
                if (File.Exists(path + "HCRPlayers_Backup.xml"))
                {
                    if (File.Exists(path + "HCRPlayers_Backup1.xml"))
                    {
                        File.Delete(path + "HCRPlayers_Backup1.xml");
                    }

                    File.Copy(path + "HCRPlayers_Backup.xml", path + "HCRPlayers_Backup1.xml");
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    
    
    public static void LoadSelected(ref List<PlayerDTO> playerDTOs, ref Player selectedPlayer)
    {

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        if (!path.EndsWith("\\")) path += "\\";
        path += "Pendulum_v3FullApp\\";
        XmlSerializer serializer = new XmlSerializer(typeof(User));

        try
        {
            if (File.Exists(path + "SelectedUser.xml"))
            {
                StreamReader reader = new StreamReader(path + "SelectedUser.xml");
                User selectedUser = (User)serializer.Deserialize(reader);

                if(selectedUser!=null)
                {
                    if (playerDTOs != null)
                    {
                        PlayerDTO tempPlayer = playerDTOs.Find(player => player.Id == selectedUser.Id);
                        if(tempPlayer!=null)
                        {
                            selectedPlayer.LoadPlayerData(tempPlayer);
                        }
                        else
                        {
                            selectedPlayer.LoadPlayerData(new PlayerDTO(selectedUser.Name, selectedUser.Surname, selectedUser.Id));
                            AddPlayer(selectedPlayer, ref playerDTOs);
                        }
                        
                    }
                    else
                    {
                        selectedPlayer.LoadPlayerData(new PlayerDTO(selectedUser.Name, selectedUser.Surname, selectedUser.Id));
                        AddPlayer(selectedPlayer, ref playerDTOs);
                    }

                   
                        
                }
                
                reader.Close();
            }
            else
            {
                if (File.Exists(path + "SelectedPlayer_Backup.xml"))
                {
                    if (File.Exists(path + "SelectedPlayer_Backup1.xml"))
                    {
                        File.Delete(path + "SelectedPlayer_Backup1.xml");
                    }
                    File.Copy(path + "SelectedPlayer_Backup.xml", path + "SelectedPlayer_Backup1.xml");
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }
    
    public static void AddPlayer(Player player, ref List<PlayerDTO> players)
    {
        XmlSerializer XMLwriter = new XmlSerializer(typeof(List<PlayerDTO>));
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        if (!path.EndsWith("\\")) path += "\\";
        path += "Pendulum_v3FullApp\\";
        System.IO.Directory.CreateDirectory(path);
        StreamWriter xmlFile = new StreamWriter(path + "HCRPlayers.xml");
        
        try
        {
            if (player != null)
            {
                players.Add(player.playerData);
                XMLwriter.Serialize(xmlFile, players);
                xmlFile.Close();
            }
        }
        catch (Exception ex)
        {
            if (xmlFile != null)
            {
                xmlFile.Close();
            }
        }
        finally
        {
            xmlFile.Close();
        }
    }

    public static void updatePlayerData(Player player)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        if (!path.EndsWith("\\")) path += "\\";
        path += "Pendulum_v3FullApp\\";
        XDocument doc = XDocument.Load(path + "HCRPlayers.xml");

        try
        {
            XElement coinsElement = doc.Root.Elements("PlayerDTO").FirstOrDefault(e=>e.Element("UserId").Value == player.playerData.Id.ToString()).Element("Coins");

            coinsElement.Value = player.playerData.Coins.ToString();
        }
        catch (Exception ex)
        {
            {
                doc.Save(path + "HCRPlayers.xml");
            }
        }
        finally
        {               
            doc.Save(path + "HCRPlayers.xml");
        }
        
    }
}
