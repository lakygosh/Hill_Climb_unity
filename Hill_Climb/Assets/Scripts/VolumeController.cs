using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class VolumeController : MonoBehaviour
{
    public static VolumeController instance;

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] public Slider musicSlider;
    [SerializeField] public Slider sfxSlider;

    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";

    public bool isMusicMuted = false;
    public bool isSFXMuted = false;

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
    }

    void SetMusicVolume(float value) 
    {
        if (value == 0.0001f)
        {
            isMusicMuted = true;
        }
        audioMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value)*20); 
    }
    void SetSFXVolume(float value)
    {
        if (value == 0.0001f)
        {
            isSFXMuted = true;
        }
        audioMixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }

    public void MuteMusic()
    {
        if (musicSlider.value == 0.0001f)
        {
            musicSlider.value = 0.5f;
            ButtonImgChange.instance.MusicBtnOn();
        }
        else 
        {
            musicSlider.value = 0.0001f;
            ButtonImgChange.instance.MusicBtnOff();
        }
    }
    public void MuteSFX()
    {
        if (sfxSlider.value == 0.0001f)
        {
            sfxSlider.value = 0.5f;
            ButtonImgChange.instance.SFXBtnOn();
        }
        else
        {
            sfxSlider.value = 0.0001f;
            ButtonImgChange.instance.SFXBtnOff();
        }
    }

    public void MusicBtnChange()
    {
        if (musicSlider.value == 0.0001f)
        {
            ButtonImgChange.instance.MusicBtnOff();
        }
        else
        {
            ButtonImgChange.instance.MusicBtnOn();
        }
    }

    public void SFXBtnChange()
    {
        if (sfxSlider.value == 0.0001f)
        {
            ButtonImgChange.instance.SFXBtnOff();
        }
        else
        {
            ButtonImgChange.instance.SFXBtnOn();
        }
    }
}
