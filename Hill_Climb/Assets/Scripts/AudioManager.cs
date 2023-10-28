using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioMixer audioMixer;
    public const string MUSIC_KEY = "MusicVolume";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }

        LoadVolume();
    }

    void LoadVolume() //Volume saved in VolumeController.cs
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        audioMixer.SetFloat(VolumeController.MIXER_MUSIC, Mathf.Log10(musicVolume)*20);
    }
}
