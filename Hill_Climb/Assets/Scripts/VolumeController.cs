using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;

    public const string MIXER_MUSIC = "MusicVolume";

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
    }

    void SetMusicVolume(float value) 
    {
        audioMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value)*20); 
    }

    public void MuteMusic()
    {
        musicSlider.value = 0;
    }
}
