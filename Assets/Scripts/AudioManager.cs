using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;

    public static AudioManager Instance { get; private set; }

    private void Start()
    {
        Instance = GetComponent<AudioManager>();

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }

    public void PlayOneSound(AudioClip audioClip)
    {
        if (_audioSource.isPlaying)
            return;

        _audioSource.PlayOneShot(audioClip);
    }
}
