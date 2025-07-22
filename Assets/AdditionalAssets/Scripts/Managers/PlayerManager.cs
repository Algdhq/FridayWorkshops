using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public enum AmmoType { melee, handgun, magnum, laser, shotgun, submachinegun, machinegun, rifle, RPG, grenade, molotov, mine, tnt};
    private AmmoType _ammoType;

    [Header("Gameplay Values")]
    [SerializeField] private int _healthPackValue;

    [Header("Connected Components")]
    [SerializeField] private PlayerStatsSO _playerStats;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _ammoInClip;
    [SerializeField] private TextMeshProUGUI _totalAmmo;
    
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
        UpdateAmmoUI();
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

    public void AddHealthPack()
    {
        int currentHealthPackCount = _playerStats.currentHealthKits;
        if (currentHealthPackCount < _playerStats.maxHealthKits)
        {
            _playerStats.currentHealthKits++;
        }
    }

    public void UseHealthPack()
    {
        int currentHealthPackCount = _playerStats.currentHealthKits;
        if (currentHealthPackCount > 0)
        {
            UpdateHealthValue(_healthPackValue);
            _playerStats.currentHealthKits--;
            AudioManager.Instance.PlayUISFXClip(4);
            UIManager.Instance.UpdateStats();
        }
        else
        {
            AudioManager.Instance.PlayUISFXClip(5);
        }
    }

    public bool CheckAmmoAvailability(int value)
    {
        if (value == 0) // melee
        {
            return true;
        }
        else if (value == 1) // handgun
        {
            return _playerStats.handgunClip > 0;
        }
        else if (value == 2) // magnum
        {
            return _playerStats.MagnumClip > 0;
        }
        else if (value == 3) // laser
        {
            return _playerStats.LaserClip > 0;
        }
        else if (value == 4) // shotgun
        {
            return _playerStats.shotgunClip > 0;
        }
        else if (value == 5) // submachinegun
        {
            return _playerStats.SubMachineGunClip > 0;
        }
        else if (value == 6) // machinegun
        {
            return _playerStats.MachineGunClip > 0;
        }
        else if (value == 7) // rifle
        {
            return _playerStats.rifleClip > 0;
        }
        else if (value == 8) // RPG
        {
            return _playerStats.RPGClip > 0;
        }
        else if (value == 9) // grenade
        {
            return _playerStats.GrenadeClip > 0;
        }
        else if (value == 10) // molotov
        {
            return _playerStats.MolotovClip > 0;
        }
        else if (value == 11) // mine
        {
            return _playerStats.MineClip > 0;
        }
        else if (value == 12) // tnt
        {
            return _playerStats.TNTClip > 0;
        }
        else
        {
            return false;
        }
    }


    public void UseHandgunBullet()
    {
        _playerStats.handgunClip = Mathf.Clamp(_playerStats.handgunClip - 1, 0, _playerStats.maxHandgunAmmo);
        UpdateAmmoUI();
    }

    public void UseMagnumBullet()
    {
        _playerStats.MagnumClip = Mathf.Clamp(_playerStats.MagnumClip - 1, 0, _playerStats.maxMagnumAmmo);
        UpdateAmmoUI();
    }

    public void UseLaserBullet()
    {
        _playerStats.LaserClip = Mathf.Clamp(_playerStats.LaserClip - 1, 0, _playerStats.maxLaserAmmo);
        UpdateAmmoUI();
    }

    public void UseShotgunBullet()
    {
        _playerStats.shotgunClip = Mathf.Clamp(_playerStats.shotgunClip - 1, 0, _playerStats.maxShotgunAmmo);
        UpdateAmmoUI();
    }

    public void UseSubMachineGunBullet()
    {
        _playerStats.SubMachineGunClip = Mathf.Clamp(_playerStats.SubMachineGunClip - 1, 0, _playerStats.maxSubMachineGunAmmo);
        UpdateAmmoUI();
    }

    public void UseMachineGunBullet()
    {
        _playerStats.MachineGunClip = Mathf.Clamp(_playerStats.MachineGunClip - 1, 0, _playerStats.maxMachineGunAmmo);
        UpdateAmmoUI();
    }

    public void UseRifleBullet()
    {
        _playerStats.rifleClip = Mathf.Clamp(_playerStats.rifleClip - 1, 0, _playerStats.maxRifleAmmo);
        UpdateAmmoUI();
    }

    public void UseRPGBullet()
    {
        _playerStats.RPGClip = Mathf.Clamp(_playerStats.RPGClip - 1, 0, _playerStats.maxRPGAmmo);
        UpdateAmmoUI();
    }

    public void UseGrenade()
    {
        _playerStats.GrenadeClip = Mathf.Clamp(_playerStats.GrenadeClip - 1, 0, _playerStats.maxGrenadeAmmo);
        UpdateAmmoUI();
    }

    public void UseMolotov()
    {
        _playerStats.MolotovClip = Mathf.Clamp(_playerStats.MolotovClip - 1, 0, _playerStats.maxMolotovAmmo);
        UpdateAmmoUI();
    }

    public void UseMine()
    {
        _playerStats.MineClip = Mathf.Clamp(_playerStats.MineClip - 1, 0, _playerStats.maxMineAmmo);
        UpdateAmmoUI();
    }

    public void UseTNT()
    {
        _playerStats.TNTClip = Mathf.Clamp(_playerStats.TNTClip - 1, 0, _playerStats.maxTNTAmmo);
        UpdateAmmoUI();
    }

    public void ReloadHandgun()
    {
        int needed = _playerStats.maxHandgunClip - _playerStats.handgunClip;
        int available = Mathf.Min(needed, _playerStats.currentHandgunAmmo);

        _playerStats.handgunClip += available;
        _playerStats.currentHandgunAmmo -= available;
        UpdateAmmoUI();
    }

    public void ReloadMagnum()
    {
        int needed = _playerStats.maxMagnumClip - _playerStats.MagnumClip;
        int available = Mathf.Min(needed, _playerStats.currentMagnumAmmo);

        _playerStats.MagnumClip += available;
        _playerStats.currentMagnumAmmo -= available;
        UpdateAmmoUI();
    }

    public void ReloadLaser()
    {
        int needed = _playerStats.maxLaserClip - _playerStats.LaserClip;
        int available = Mathf.Min(needed, _playerStats.currentLaserAmmo);

        _playerStats.LaserClip += available;
        _playerStats.currentLaserAmmo -= available;
        UpdateAmmoUI();
    }

    public void ReloadShotgun()
    {
        int needed = _playerStats.maxShotgunClip - _playerStats.shotgunClip;
        int available = Mathf.Min(needed, _playerStats.currentShotgunAmmo);

        _playerStats.shotgunClip += available;
        _playerStats.currentShotgunAmmo -= available;
        UpdateAmmoUI();
    }

    public void ReloadSubMachineGun()
    {
        int needed = _playerStats.maxSubMachineGunClip - _playerStats.SubMachineGunClip;
        int available = Mathf.Min(needed, _playerStats.currentSubMachineGunAmmo);

        _playerStats.SubMachineGunClip += available;
        _playerStats.currentSubMachineGunAmmo -= available;
        UpdateAmmoUI();
    }

    public void ReloadMachineGun()
    {
        int needed = _playerStats.maxMachineGunClip - _playerStats.MachineGunClip;
        int available = Mathf.Min(needed, _playerStats.currentMachineGunAmmo);

        _playerStats.MachineGunClip += available;
        _playerStats.currentMachineGunAmmo -= available;
        UpdateAmmoUI();
    }

    public void ReloadRifle()
    {
        int needed = _playerStats.maxRifleClip - _playerStats.rifleClip;
        int available = Mathf.Min(needed, _playerStats.currentRifleAmmo);

        _playerStats.rifleClip += available;
        _playerStats.currentRifleAmmo -= available;
        UpdateAmmoUI();
    }

    public void ReloadRPG()
    {
        int needed = _playerStats.maxRPGClip - _playerStats.RPGClip;
        int available = Mathf.Min(needed, _playerStats.currentRPGAmmo);

        _playerStats.RPGClip += available;
        _playerStats.currentRPGAmmo -= available;
        UpdateAmmoUI();
    }

    public void ReloadGrenade()
    {
        int needed = _playerStats.maxGrenadeClip - _playerStats.GrenadeClip;
        int available = Mathf.Min(needed, _playerStats.currentGrenadeAmmo);

        _playerStats.GrenadeClip += available;
        _playerStats.currentGrenadeAmmo -= available;
        UpdateAmmoUI();
    }

    public void ReloadMolotov()
    {
        int needed = _playerStats.maxMolotovClip - _playerStats.MolotovClip;
        int available = Mathf.Min(needed, _playerStats.currentMolotovAmmo);

        _playerStats.MolotovClip += available;
        _playerStats.currentMolotovAmmo -= available;
        UpdateAmmoUI();
    }

    public void ReloadMine()
    {
        int needed = _playerStats.maxMineClip - _playerStats.MineClip;
        int available = Mathf.Min(needed, _playerStats.currentMineAmmo);

        _playerStats.MineClip += available;
        _playerStats.currentMineAmmo -= available;
        UpdateAmmoUI();
    }

    public void ReloadTNT()
    {
        int needed = _playerStats.maxTNTClip - _playerStats.TNTClip;
        int available = Mathf.Min(needed, _playerStats.currentTNTAmmo);

        _playerStats.TNTClip += available;
        _playerStats.currentTNTAmmo -= available;
        UpdateAmmoUI();
    }


    public void UpdateHandgunAmmo(int value)
    {
        _playerStats.currentHandgunAmmo += value;
        _playerStats.currentHandgunAmmo = Mathf.Clamp(_playerStats.currentHandgunAmmo, 0, _playerStats.maxHandgunAmmo);
    }

    public void UpdateMagnumAmmo(int value)
    {
        _playerStats.currentMagnumAmmo += value;
        _playerStats.currentMagnumAmmo = Mathf.Clamp(_playerStats.currentMagnumAmmo, 0, _playerStats.maxMagnumAmmo);
    }

    public void UpdateLaserAmmo(int value)
    {
        _playerStats.currentLaserAmmo += value;
        _playerStats.currentLaserAmmo = Mathf.Clamp(_playerStats.currentLaserAmmo, 0, _playerStats.maxLaserAmmo);
    }

    public void UpdateShotgunAmmo(int value)
    {
        _playerStats.currentShotgunAmmo += value;
        _playerStats.currentShotgunAmmo = Mathf.Clamp(_playerStats.currentShotgunAmmo, 0, _playerStats.maxShotgunAmmo);
    }

    public void UpdateSubMachineGunAmmo(int value)
    {
        _playerStats.currentSubMachineGunAmmo += value;
        _playerStats.currentSubMachineGunAmmo = Mathf.Clamp(_playerStats.currentSubMachineGunAmmo, 0, _playerStats.maxSubMachineGunAmmo);
    }

    public void UpdateMachineGunAmmo(int value)
    {
        _playerStats.currentMachineGunAmmo += value;
        _playerStats.currentMachineGunAmmo = Mathf.Clamp(_playerStats.currentMachineGunAmmo, 0, _playerStats.maxMachineGunAmmo);
    }

    public void UpdateRifleAmmo(int value)
    {
        _playerStats.currentRifleAmmo += value;
        _playerStats.currentRifleAmmo = Mathf.Clamp(_playerStats.currentRifleAmmo, 0, _playerStats.maxRifleAmmo);
    }
    public void UpdateRPGBulletAmmo(int value)
    {
        _playerStats.currentRPGAmmo += value;
        _playerStats.currentRPGAmmo = Mathf.Clamp(_playerStats.currentRPGAmmo, 0, _playerStats.maxRPGAmmo);
    }

    public void UpdateGrenadeAmmo(int value)
    {
        _playerStats.currentGrenadeAmmo += value;
        _playerStats.currentGrenadeAmmo = Mathf.Clamp(_playerStats.currentGrenadeAmmo, 0, _playerStats.maxGrenadeAmmo);
    }

    public void UpdateMolotovAmmo(int value)
    {
        _playerStats.currentMolotovAmmo += value;
        _playerStats.currentMolotovAmmo = Mathf.Clamp(_playerStats.currentMolotovAmmo, 0, _playerStats.maxMolotovAmmo);
    }

    public void UpdateMineAmmo(int value)
    {
        _playerStats.currentMineAmmo += value;
        _playerStats.currentMineAmmo = Mathf.Clamp(_playerStats.currentMineAmmo, 0, _playerStats.maxMineAmmo);
    }

    public void UpdateTNTAmmo(int value)
    {
        _playerStats.currentTNTAmmo += value;
        _playerStats.currentTNTAmmo = Mathf.Clamp(_playerStats.currentTNTAmmo, 0, _playerStats.maxTNTAmmo);
    }

    public void UpdateAmmoUI()
    {
        switch (_ammoType)
        {
            case AmmoType.melee:
                _ammoInClip.text = "∞";
                _totalAmmo.text = "/∞";
                break;

            case AmmoType.handgun:
                _ammoInClip.text = _playerStats.handgunClip.ToString();
                _totalAmmo.text = "/ " + _playerStats.currentHandgunAmmo.ToString();
                break;

            case AmmoType.magnum:
                _ammoInClip.text = _playerStats.MagnumClip.ToString();
                _totalAmmo.text = "/ " + _playerStats.currentMagnumAmmo.ToString();
                break;

            case AmmoType.laser:
                _ammoInClip.text = "∞";
                _totalAmmo.text = "/∞";
                break;

            case AmmoType.shotgun:
                _ammoInClip.text = _playerStats.shotgunClip.ToString();
                _totalAmmo.text = "/ " + _playerStats.currentShotgunAmmo.ToString();
                break;

            case AmmoType.submachinegun:
                _ammoInClip.text = _playerStats.SubMachineGunClip.ToString();
                _totalAmmo.text = "/ " + _playerStats.currentSubMachineGunAmmo.ToString();
                break;

            case AmmoType.machinegun:
                _ammoInClip.text = _playerStats.MachineGunClip.ToString();
                _totalAmmo.text = "/ " + _playerStats.currentMachineGunAmmo.ToString();
                break;

            case AmmoType.rifle:
                _ammoInClip.text = _playerStats.rifleClip.ToString();
                _totalAmmo.text = "/ " + _playerStats.currentRifleAmmo.ToString();
                break;

            case AmmoType.RPG:
                _ammoInClip.text = _playerStats.RPGClip.ToString();
                _totalAmmo.text = "/ " + _playerStats.currentRPGAmmo.ToString();
                break;

            case AmmoType.grenade:
                _ammoInClip.text = _playerStats.GrenadeClip.ToString();
                _totalAmmo.text = "/ " + _playerStats.currentGrenadeAmmo.ToString();
                break;

            case AmmoType.molotov:
                _ammoInClip.text = _playerStats.MolotovClip.ToString();
                _totalAmmo.text = "/ " + _playerStats.currentMolotovAmmo.ToString();
                break;

            case AmmoType.mine:
                _ammoInClip.text = _playerStats.MineClip.ToString();
                _totalAmmo.text = "/ " + _playerStats.currentMineAmmo.ToString();
                break;

            case AmmoType.tnt:
                _ammoInClip.text = _playerStats.TNTClip.ToString();
                _totalAmmo.text = "/ " + _playerStats.currentTNTAmmo.ToString();
                break;
        }
    }


    public void SetAmmoType(AmmoType type)
    {
        _ammoType = type;
    }

    public PlayerStatsSO GetStats()
    {
        return _playerStats;
    }
}
