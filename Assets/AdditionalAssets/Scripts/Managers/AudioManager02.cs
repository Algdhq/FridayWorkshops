using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager02 : MonoBehaviour
{
    public static AudioManager02 Instance { get; private set; }

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] _UISFXClips;
    [SerializeField] private AudioClip[] _ambienceClips;
    [SerializeField] private AudioClip[] _musicClips;
    [SerializeField] private AudioClip[] _weaponClips;
    [SerializeField] private AudioClip[] _playerVOClips;

    [Header("AudioSources")]
    [SerializeField] private AudioSource _UISFXSource;
    [SerializeField] private AudioSource _ambienceSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _weaponSource;
    [SerializeField] private AudioSource _playerVOSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public void PlayUISFXClip(int value)
    {
        if(value < 0 || value >= _UISFXClips.Length)
        {
            _UISFXSource.Pause();
            return;
        }
        _UISFXSource.clip = _UISFXClips[value];
        _UISFXSource.Play();
    }

    public void PlayAmbienceClip(int value)
    {
        if (value < 0 || value >= _ambienceClips.Length)
        {
            _ambienceSource.Pause();
            return;
        }
        _ambienceSource.clip = _ambienceClips[value];
        _ambienceSource.Play();
    }
}
