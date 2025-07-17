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
    private Animator _playerAnim;
    private bool _isReloading;

    private void Start()
    {
        _playerAnim = GameObject.Find("PlayerArmature").GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isReloading == true)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
        {
            if (PlayerManager.Instance.CheckAmmoAvailability(2))//Verify slot number in inventory manager
            {
                FireGun();
                PlayerManager.Instance.UseMagnumBullet();
            }
            else
            {
                AudioManager.Instance.PlayWeaponClip(4);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadRoutine());
        }
    }

    public void FireGun()
    {
        _anim.SetTrigger("FireColt");
        _muzzleFlash.Play();
        _muzzleFlash02.Play();
        _smoke.Play();
        StartCoroutine("FlashLight");
        PlayerManager.Instance.CamShake(PlayerManager.ShakeStrength.Weak);
        AudioManager.Instance.PlayWeaponClip(0);
        _event.Invoke();
    }

    IEnumerator FlashLight()
    {
        _light.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _light.SetActive(false);
    }

    IEnumerator ReloadRoutine()
    {
        _isReloading = true;
        PlayerManager.Instance.ReloadMagnum();
        _playerAnim.SetTrigger("Reload");
        AudioManager.Instance.PlayWeaponClip(5);
        yield return new WaitForSeconds(1.4f);
        _isReloading = false;
    }
}
