using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MP5KAttack : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private ParticleSystem _muzzleFlash01;
    [SerializeField] private ParticleSystem _muzzleFlash02;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private ParticleSystem _shellEjection;
    [SerializeField] private GameObject _light;
    [SerializeField] private UnityEvent _event;
    private Animator _playerAnim;
    private bool _isReloading;
    private bool _isFiring;


    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
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
            Debug.Log("Am I firing");
            if (PlayerManager.Instance.CheckAmmoAvailability(5))//Verify slot number in inventory manager
            {
                _isFiring = true;
                StartCoroutine("FireAutomatic");
            }
            else
            {
                _isFiring = false;
                StopCoroutine("FireAutomatic");
                AudioManager.Instance.PlayWeaponClip(4);
            }
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            _isFiring = false;
            StopCoroutine(FireAutomatic());
            _muzzleFlash01.Stop();
            _muzzleFlash02.Stop();
            _shellEjection.Stop();
            _smoke.Stop();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadRoutine());
        }
    }

    IEnumerator FireAutomatic()
    {
        while (_isFiring)
        {
            if (PlayerManager.Instance.CheckAmmoAvailability(5))
            {
                FireGun();
                PlayerManager.Instance.UseSubMachineGunBullet();
            }
            else
            {
                AudioManager.Instance.PlayWeaponClip(4);
                _isFiring = false;
                _muzzleFlash01.Stop();
                _muzzleFlash02.Stop();
                _shellEjection.Stop();
                _smoke.Stop();
                break;
            }

            yield return new WaitForSeconds(0.15f);
        }
    }

    public void FireGun()
    {
        _anim.SetTrigger("Fire");
        _muzzleFlash01.Play();
        _muzzleFlash02.Play();
        _shellEjection.Play();
        _smoke.Play();
        PlayerManager.Instance.CamShake(PlayerManager.ShakeStrength.Weak);
        AudioManager.Instance.PlayWeaponClip(13);
        _event.Invoke();
    }

    IEnumerator ReloadRoutine()
    {
        _isReloading = true;
        PlayerManager.Instance.ReloadSubMachineGun();
        _playerAnim.SetTrigger("Reload");
        AudioManager.Instance.PlayWeaponClip(5);
        yield return new WaitForSeconds(1.4f);
        _isReloading = false;
    }
}
