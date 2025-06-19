using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColtFiring : MonoBehaviour
{
    [SerializeField] private ParticleSystem _muzzleFlash01;
    [SerializeField] private ParticleSystem _muzzleFlash02;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioSource _audioSource;
    private Animator _anim;    

    
    // Start is called before the first frame update

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _anim.SetTrigger("Fire");
            _muzzleFlash01.Play();
            _muzzleFlash02.Play();
            _smoke.Play();
            _audioSource.clip = _audioClip;
            _audioSource.Play();
            Debug.Log("I'm firing my weapon");
        }
    }
}
