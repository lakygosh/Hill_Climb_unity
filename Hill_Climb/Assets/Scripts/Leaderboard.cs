using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI first;
    [SerializeField]
    private TextMeshProUGUI second;
    [SerializeField]
    private TextMeshProUGUI third;
    
    [SerializeField]
    private List<TextMeshProUGUI> rank;
    [SerializeField]
    private List<TextMeshProUGUI> name;
    [SerializeField]
    private List<TextMeshProUGUI> score;
    
    // Start is called before the first frame update
    private void Awake()
    {
        GetLeaderboard(PlayerManager.GetPlayerDTOs());
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Leaderboard");
    }
    
    public void GetLeaderboard(List<PlayerDTO> playerDTOs)
    {
        for (int i = 3; i < 10; i++)
        {
            rank[i-3].text = (i+1).ToString();
        }

        if (playerDTOs==null || playerDTOs.Count==0)
        {
            return;
        }
        playerDTOs.Sort((p1,p2) => p2.BestScore.CompareTo(p1.BestScore));
        first.text = playerDTOs.First().Name + ": \n" + playerDTOs.First().BestScore.ToString("f0");
        if(playerDTOs.Count==1)
        {
            return;
        }
        second.text = playerDTOs[1].Name + ": \n" + playerDTOs[1].BestScore.ToString("f0");
        if(playerDTOs.Count==2)
        {
            return;
        }
        third.text = playerDTOs[2].Name + ": \n" + playerDTOs[2].BestScore.ToString("f0");
        
        for (int i = 3; i < playerDTOs.Count; i++)
        {
            name[i-3].text = playerDTOs[i].Name;
            score[i-3].text = playerDTOs[i].BestScore.ToString("f0");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
