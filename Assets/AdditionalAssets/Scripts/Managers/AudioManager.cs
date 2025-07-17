using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] _sfxClips;
    [SerializeField] private AudioClip[] _weaponClips;
    [SerializeField] private AudioClip[] _stingerClips;
    [SerializeField] private AudioClip[] _longStingerClips;
    [SerializeField] private AudioClip[] _footstepClips;
    [SerializeField] private AudioClip[] _enemyClips;
    [SerializeField] private AudioClip[] _UISFXClips;
    [SerializeField] private AudioClip[] _UISFXMovementClip;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _weaponSource;
    [SerializeField] private AudioSource _stingerSource;
    [SerializeField] private AudioSource _longStingerSource;
    [SerializeField] private AudioSource _footstepAudioSource;
    [SerializeField] private AudioSource _enemyAudioSource;
    [SerializeField] private AudioSource _UISFXAudioSource;
    [SerializeField] private AudioSource _UISFXMovementSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }
        Instance = this;
    }

    public void PlaySFXClip(int value)
    {
        if (value < 0 || value >= _sfxClips.Length)
        {
            _sfxAudioSource.Pause();
            return;
        }
        _sfxAudioSource.clip = _sfxClips[value];
        _sfxAudioSource.Play();
    }

    public void PlayWeaponClip(int value)
    {
        if (value < 0 || value >= _weaponClips.Length)
        {
            _weaponSource.Pause();
            return;
        }
        _weaponSource.clip = _weaponClips[value];
        _weaponSource.Play();
    }

    public void PlayStingerClip(int value)
    {
        if (value < 0 || value >= _stingerClips.Length)
        {
            _stingerSource.Pause();
            return;
        }
        _stingerSource.clip = _stingerClips[value];
        _stingerSource.Play();
    }

    public void PlayLongStingerClip(int value)
    {
        if (value < 0 || value >= _longStingerClips.Length)
        {
            _longStingerSource.Pause();
            return;
        }
        _longStingerSource.clip = _longStingerClips[value];
        _longStingerSource.Play();
    }

    public void PlayFootstepClip(int value)
    {
        if (value < 0 || value >= _footstepClips.Length)
        {
            _footstepAudioSource.Pause();
            return;
        }
        _footstepAudioSource.clip = _footstepClips[value];
        _footstepAudioSource.Play();
    }

    public void PlayEnemyClip(int value)
    {
        if (value < 0 || value >= _enemyClips.Length)
        {
            _enemyAudioSource.Pause();
            return;
        }
        _enemyAudioSource.clip = _enemyClips[value];
        _enemyAudioSource.Play();
    }

    public void PlayUISFXClip(int value)
    {
        if (value < 0 || value >= _UISFXClips.Length)
        {
            _UISFXAudioSource.Pause();
            return;
        }
        _UISFXAudioSource.clip = _UISFXClips[value];
        _UISFXAudioSource.Play();
    }

    public void PlayUISFXMovement(int value)
    {
        if (value < 0 || value >= _UISFXMovementClip.Length)
        {
            _UISFXMovementSource.Pause();
            return;
        }
        _UISFXMovementSource.clip = _UISFXMovementClip[value];
        _UISFXMovementSource.Play();
    }
}


    
