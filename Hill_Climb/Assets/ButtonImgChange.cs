using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImgChange : MonoBehaviour
{
    public static ButtonImgChange instance;
    public Image currentMusicImg;

    public Image currentSFXImg;

    public Sprite musicOn;
    public Sprite musicOff;
    public Sprite sfxOn;
    public Sprite sfxOff;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void MusicBtnChange()
    {
        if (currentMusicImg.sprite == musicOn)
        {
            currentMusicImg.sprite = musicOff;
        }
        else
        {
            currentMusicImg.sprite = musicOn;
        }
    }

    public void SFXBtnChange()
    {
        if (currentSFXImg.sprite.Equals(sfxOn))
        {
            currentSFXImg.sprite = sfxOff;
        }
        else
        {
            currentSFXImg.sprite = sfxOn;
        }
    }

    public void SFXBtnOn() { currentSFXImg.sprite = sfxOn; }
    public void SFXBtnOff() { currentSFXImg.sprite = sfxOff; }
    public void MusicBtnOn() { currentMusicImg.sprite = musicOn; }
    public void MusicBtnOff() { currentMusicImg.sprite = musicOff; }
    

}
