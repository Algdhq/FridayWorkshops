using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("SFX Clips")]
    [SerializeField] private AudioClip[] _sfxClips;
    [Header("Footstep Clips")]
    [SerializeField] private AudioClip[] _footstepClips;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _footstepAudioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: persist across scenes
    }

    public void PlaySFXClip(int value)
    {
        _sfxAudioSource.clip = _sfxClips[value];
        _sfxAudioSource.Play();
    }

    public void PlayFootstepClip(int value)
    {
        _footstepAudioSource.clip = _footstepClips[value];
        _footstepAudioSource.Play();
    }
}
