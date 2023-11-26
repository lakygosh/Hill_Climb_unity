using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController instance = new SFXController();
    public AudioSource src;
    public AudioClip positiveButtonClik, negativeButtonClick, coinCollect, fuelCollect, jumpNotification, jumpSound, correct, waterSplash, carUnlockSound, gameOver;


    private void Awake()
    {
        if (instance == null)
        { 
            instance = this;
        }
    }
    public void PositiveButtonClik()
    {
        src.clip = positiveButtonClik;
        src.Play();
    }
    public void NegativeButtonClick()
    {
        src.clip = negativeButtonClick;
        src.Play();
    }
    public void CoinCollect()
    {
        src.clip = coinCollect;
        src.Play();
    }
    public void FuelCollect()
    {
        src.clip = fuelCollect;
        src.Play();
    }
    public void JumpNotification()
    {
        src.clip = jumpNotification;
        src.Play();
    }
    public void JumpSound()
    {
        src.clip = jumpSound;
        src.Play();
    }
    public void Correct()
    {
        src.clip = correct;
        src.Play();
    }
    public void WaterSplash()
    {
        src.clip = waterSplash;
        src.Play();
    }
    public void CarUnlockSound()
    {
        src.clip = carUnlockSound;
        src.Play();
    }
    public void GameOver()
    {
        src.clip = gameOver;
        src.Play();
    }
}
