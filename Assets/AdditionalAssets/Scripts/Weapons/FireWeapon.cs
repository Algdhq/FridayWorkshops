using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private GameObject _light;
    [SerializeField] private ParticleSystem _muzzleFlash02;
    [SerializeField] private UnityEvent _event;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
        {
            FireGun();
        }
    }

    public void FireGun()
    {
        _anim.SetTrigger("FireColt");
        _muzzleFlash.Play();
        _muzzleFlash02.Play();
        _smoke.Play();
        StartCoroutine("FlashLight");
        PlayerManager.Instance.CamShake();
        AudioManager.Instance.PlaySFXClip(0);
        _event.Invoke();
    }

    IEnumerator FlashLight()
    {
        _light.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _light.SetActive(false);
    }
}
