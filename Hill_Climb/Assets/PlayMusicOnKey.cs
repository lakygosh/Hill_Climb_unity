using UnityEngine;

public class PlayAudioOnKeyPress : MonoBehaviour
{
    public AudioClip audioClip; // Assign your audio clip in the Unity Editor
    private AudioSource audioSource;

    void Start()
    {
        // Ensure there is an AudioSource component on this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the audio clip to the AudioSource
        audioSource.clip = audioClip;
    }

    void Update()
    {
        // Check if the "C" key is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Play the audio clip
            audioSource.Play();
        }
    }
}
