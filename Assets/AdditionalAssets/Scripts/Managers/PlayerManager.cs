using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public enum AmmoType { handgun, shotgun, rifle};
    private AmmoType _ammoType;

    [Header("Connected Components")]
    [SerializeField] private PlayerStatsSO _playerStats;
    [SerializeField] private TextMeshProUGUI _healthText;
    
    private CinemachineImpulseSource _impulse;
    public static class ShakeStrength
    {
        public static readonly float Weak = 0.1f;
        public static readonly float Normal = 0.2f;
        public static readonly float Strong = 0.4f;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _impulse = GameObject.Find("PlayerArmature").GetComponent<CinemachineImpulseSource>();
        _healthText.text = "Health: " + _playerStats.currentHealth.ToString();
    }

    public void CamShake(float strength = 1f)
    {
        _impulse.GenerateImpulse(Vector3.one * strength);
    }

    public void UpdateHealthValue(int value)//+20 health
    {
        _playerStats.currentHealth += value;
        _playerStats.currentHealth = Mathf.Clamp(_playerStats.currentHealth, 0, 100);
        _healthText.text = "Health: " + _playerStats.currentHealth.ToString();
        if (_playerStats.currentHealth <= 0)
        {
            Debug.Log("I died - now play death function");
        }
    }

    public bool CheckAmmoAvailability(int value)
    {
        if (value == 0)
        {
            return _playerStats.handgunClip > 0;
        }
        else if (value == 1)
        {
            return _playerStats.shotgunClip > 0;
        }
        else if (value == 2)
        {
            return _playerStats.rifleClip > 0;
        }
        else return false;        
    }

    public void UseHandgunBullet()
    {
        _playerStats.handgunClip = Mathf.Clamp(_playerStats.handgunClip - 1, 0, _playerStats.maxHandgunAmmo);
    }

    public void UseShotgunBullet()
    {
        _playerStats.shotgunClip = Mathf.Clamp(_playerStats.shotgunClip - 1, 0, _playerStats.maxShotgunAmmo);
    }

    public void UseRifleBullet()
    {
        _playerStats.rifleClip = Mathf.Clamp(_playerStats.rifleClip - 1, 0, _playerStats.maxRifleAmmo);
    }

    public void ReloadHandgun()
    {
        int needed = _playerStats.maxHandgunClip - _playerStats.handgunClip;
        int available = Mathf.Min(needed, _playerStats.currentHandgunAmmo);

        _playerStats.handgunClip += available;
        _playerStats.currentHandgunAmmo -= available;
    }

    public void UpdateHandgunAmmo(int value)
    {
        _playerStats.currentHandgunAmmo += value;
        _playerStats.currentHandgunAmmo = Mathf.Clamp(_playerStats.currentHandgunAmmo, 0, _playerStats.maxHandgunAmmo);
    }

    public void UpdateShotgunAmmo(int value)
    {
        _playerStats.currentShotgunAmmo += value;
        _playerStats.currentShotgunAmmo = Mathf.Clamp(_playerStats.currentShotgunAmmo, 0, _playerStats.maxShotgunAmmo);
    }

    public void UpdateRifleAmmo(int value)
    {
        _playerStats.currentRifleAmmo += value;
        _playerStats.currentRifleAmmo = Mathf.Clamp(_playerStats.currentRifleAmmo, 0, _playerStats.maxRifleAmmo);
    }
}
