using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootElectricPanels : MonoBehaviour
{
    [SerializeField] private ShootElectricPanelPuzzle _panelPuzzle;
    [SerializeField] private bool _useHitpoints;
    [SerializeField] private int _hitPoints;

    [SerializeField] private GameObject _decal;
    [SerializeField] private GameObject _light;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioClip _damageAudioClip;
    [SerializeField] private Renderer _material;
    [SerializeField] private Material _newMaterial;
    [SerializeField] private GameObject _lightSource;
    
    [SerializeField] private AudioSource _audioSource;
    public bool _isPanelBroken;

    [Header("Event played after destroying")]
    [SerializeField] private UnityEvent _event;
    //private CameraShake _camShake;

    // Start is called before the first frame update
    void Start()
    {
        //_camShake = GameObject.Find("PlayerArmature").GetComponent<CameraShake>();
        if (_decal != null)
        {
            _decal.SetActive(false);
        }
        if (_light != null)
        {
            _light.SetActive(true);
        }
        if (_audioSource != null)
        {
            _audioSource = GetComponent<AudioSource>();
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PanelShot(int value)
    {
        if (_isPanelBroken == false)
        {
            if (_useHitpoints == true)
            {
                int damageTaken = value;
                _hitPoints = _hitPoints - damageTaken;
                _audioSource.clip = _damageAudioClip;
                _audioSource.PlayOneShot(_damageAudioClip);

                if (_hitPoints <= 0)
                {
                    if (_decal != null)
                    {
                        _decal.SetActive(true);
                    }
                    if (_light != null)
                    {
                        _light.SetActive(false);
                    }
                    if (_particleSystem != null)
                    {
                        _particleSystem.gameObject.SetActive(true);
                        _particleSystem.Play();
                    }
                    if (_audioSource != null)
                    {
                        _audioSource.clip = _audioClip;
                        _audioSource.PlayOneShot(_audioClip);
                    }
                    if (_lightSource != null)
                    {
                        LightExplosionOn();
                    }

                    _isPanelBroken = true;
                    _panelPuzzle.checkPanelStatus();
                    Invoke("TurnOffAudio", 2.0f);
                    Invoke("LightExplosionOff", 0.2f);
                    _event.Invoke();
                    //_camShake.CamShake();
                    SwapMaterial();
                }
            }
            

            else
            {
                if (_decal != null)
                {
                    _decal.SetActive(true);
                }
                if (_light != null)
                {
                    _light.SetActive(false);
                }
                if (_particleSystem != null)
                {
                    _particleSystem.gameObject.SetActive(true);
                    _particleSystem.Play();
                }
                if (_audioSource != null)
                {
                    _audioSource.clip = _audioClip;
                    _audioSource.PlayOneShot(_audioClip);
                    Debug.Log("Audio played");
                }
                if (_lightSource != null)
                {
                    LightExplosionOn();
                }

                _isPanelBroken = true;
                _panelPuzzle.checkPanelStatus();
                Invoke("TurnOffAudio", 3.0f);
                Invoke("LightExplosionOff", 0.2f);
                _event.Invoke();
                //_camShake.CamShake();
                SwapMaterial();
            }
        }        
    }

    public void TurnOffAudio()
    {
        if (_audioSource != null)
        {
            _audioSource.enabled = false;
        }        
    }

    public void SwapMaterial()
    {
        if (_material != null && _newMaterial != null)
        {
            // Swap the material by assigning the new material to the renderer
            _material.material = _newMaterial;
        }
        else
        {
            Debug.LogWarning("Renderer or newMaterial is not assigned!");
        }
    }

    public void LightExplosionOn()
    {
        _lightSource.SetActive(true);
    }

    public void LightExplosionOff()
    {
        _lightSource.SetActive(false);
    }
}
