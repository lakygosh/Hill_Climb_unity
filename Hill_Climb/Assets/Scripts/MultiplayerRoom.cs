using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerRoom : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player1Name;
    [SerializeField] private TextMeshProUGUI player2Name;
    [SerializeField] private SpriteRenderer player2Online;
    [SerializeField] private Button backBtn;
    private float time=0;


    // Update is called once per frame
    void Update()
    {
        if (player2Name.text!="Player 2")
        {
            player2Online.color=Color.green;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBack();
        }
        
        
    }

    private void FixedUpdate()
    {
        if (time > 3.0f)
        {
            SceneManager.LoadScene("LocalMultiPlayer");
        }
        else
        {
            time += Time.fixedDeltaTime;
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}