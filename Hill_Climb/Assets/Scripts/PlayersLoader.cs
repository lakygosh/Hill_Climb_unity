using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Car;
using Pendulum_v3FullApp.Base;
using UnityEngine;

public class PlayersLoader : MonoBehaviour
{
    public static void LoadAllPlayers(ref List<Player> players)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        if (!path.EndsWith("\\")) path += "\\";
        path += "Pendulum_v3FullApp\\";
        XmlSerializer serializer = new XmlSerializer(typeof(List<User>));

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
                players = (List<Player>)serializer.Deserialize(reader);
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
    
    public static void StoreAllPlayers(List<Player> players)
    {
        XmlSerializer XMLwriter = new XmlSerializer(typeof(List<Player>));

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        if (!path.EndsWith("\\")) path += "\\";
        path += "Pendulum_v3FullApp\\";
        System.IO.Directory.CreateDirectory(path);
        StreamWriter XMLfile = new StreamWriter(path + "HCRPlayers.xml");

        try
        {
            if (players != null)
            {
                XMLwriter.Serialize(XMLfile, players);
                XMLfile.Close();
            }
            else
            {
                XMLfile.Close();
                File.Delete(path + "HCRPlayers.xml");
            }
        }
        catch (Exception ex)
        {
            if (XMLfile != null)
            {
                XMLfile.Close();
            }
        }
        finally
        {
            XMLfile.Close();
        }
    }

    public static void LoadSelected(ref List<Player> players, ref Player selectedPlayer)
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
                    if (players != null)
                    {
                        Player tempPlayer = players.Find(player => player.Id == selectedUser.Id);
                        if(tempPlayer!=null)
                        {
                            selectedPlayer = tempPlayer;
                        }
                        else
                        {
                            selectedPlayer.Name = selectedUser.Name;
                            selectedPlayer.Surname = selectedUser.Surname;
                            selectedPlayer.Id = selectedUser.Id;
                            selectedPlayer.Coins = 0;
                            selectedPlayer.Score = 0;
                        }
                        
                    }
                    else
                    {
                        selectedPlayer.Name = selectedUser.Name;
                        selectedPlayer.Surname = selectedUser.Surname;
                        selectedPlayer.Id = selectedUser.Id;
                        selectedPlayer.Coins = 0;
                        selectedPlayer.Score = 0;
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
}
