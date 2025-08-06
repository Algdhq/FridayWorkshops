using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShotgunAttack : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private ParticleSystem _muzzleFlash01;
    [SerializeField] private ParticleSystem _muzzleFlash02;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private UnityEvent _event;
    private bool _isReloading;
    private Animator _playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _playerAnim = GameObject.Find("PlayerArmature").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isReloading == true)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
        {
            if (PlayerManager.Instance.CheckAmmoAvailability(4))//Verify slot number in inventory manager
            {
                FireGun();
                PlayerManager.Instance.UseShotgunBullet();
            }
            else
            {
                AudioManager.Instance.PlayWeaponClip(4);//reload sound
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadRoutine());
        }
    }
    private void FireGun()
    {
        _anim.SetTrigger("Fire");
        _muzzleFlash01.Play();
        _muzzleFlash02.Play();
        _smoke.Play();
        _event.Invoke();
        AudioManager.Instance.PlayWeaponClip(14);
        PlayerManager.Instance.CamShake(PlayerManager.ShakeStrength.Normal);
    }

    IEnumerator ReloadRoutine()
    {
        _isReloading = true;
        PlayerManager.Instance.ReloadShotgun();
        _playerAnim.SetTrigger("Reload");
        AudioManager.Instance.PlayWeaponClip(5);
        yield return new WaitForSeconds(1.4f);
        _isReloading = false;
    }
}
