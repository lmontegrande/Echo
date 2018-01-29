using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance = null;
    public AudioClip deathAudioClip;
    public AudioClip introPingAudioClip;

    private AudioSource _audioSource;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayDeathAudioClip()
    {
        if (_audioSource.isPlaying)
            _audioSource.Stop();
        _audioSource.PlayOneShot(deathAudioClip);
    }

    public void PlayIntroAudioClip()
    {
        _audioSource.PlayOneShot(introPingAudioClip);
    }
}
