using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Melee,
    Handgun,
    Magnum,
    Laser,
    Shotgun,
    SubMachineGun,
    MachineGun,
    Rifle,
    RPG,
    Grenade,
    Molotov,
    Mine,
    TNT,
    EmptyHand
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public List<KeySO> keyItems = new List<KeySO>();
    private int _weaponIndex;
    public WeaponType _weaponType;
    [SerializeField] private List<GameObject> _weapons = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {        
        _weapons[_weaponIndex].SetActive(true);
        SetWeaponType(_weaponIndex);
    }

    public void AddKey(KeySO key)
    {
        if(!keyItems.Contains(key))
        {
            keyItems.Add(key);
        }        
    }

    public bool UseKey(string Value)
    {
        foreach(KeySO k in keyItems)
        {
            if(k.itemName == Value)
            {
                keyItems.Remove(k);
                return true;
            }
        }
        return false;        
    }

    public void SetCurrentWeapon(int value)
    {
        foreach(var w in _weapons)
        {
            w.SetActive(false);
        }
        _weaponIndex = value;
        _weapons[_weaponIndex].SetActive(true);
        SetWeaponType(value);
        UIManager.Instance.CloseInventoryMenu();

        switch (_weaponType)
        {
            case WeaponType.Melee:
                PlayerManager.Instance.SetAmmoType(PlayerManager.AmmoType.melee);
                break;
            case WeaponType.Handgun:
                PlayerManager.Instance.SetAmmoType(PlayerManager.AmmoType.handgun);
                break;
            case WeaponType.Magnum:
                PlayerManager.Instance.SetAmmoType(PlayerManager.AmmoType.magnum);
                break;
            case WeaponType.Laser:
                PlayerManager.Instance.SetAmmoType(PlayerManager.AmmoType.laser);
                break;
            case WeaponType.Shotgun:
                PlayerManager.Instance.SetAmmoType(PlayerManager.AmmoType.shotgun);
                break;
            case WeaponType.SubMachineGun:
                PlayerManager.Instance.SetAmmoType(PlayerManager.AmmoType.submachinegun);
                break;
            case WeaponType.MachineGun:
                PlayerManager.Instance.SetAmmoType(PlayerManager.AmmoType.machinegun);
                break;
            case WeaponType.Rifle:
                PlayerManager.Instance.SetAmmoType(PlayerManager.AmmoType.rifle);
                break;
            case WeaponType.RPG:
                PlayerManager.Instance.SetAmmoType(PlayerManager.AmmoType.RPG);
                break;
            case WeaponType.Grenade:
                PlayerManager.Instance.SetAmmoType(PlayerManager.AmmoType.grenade);
                break;
            case WeaponType.Molotov:
                PlayerManager.Instance.SetAmmoType(PlayerManager.AmmoType.molotov);
                break;
            case WeaponType.Mine:
                PlayerManager.Instance.SetAmmoType(PlayerManager.AmmoType.mine);
                break;
            case WeaponType.TNT:
                PlayerManager.Instance.SetAmmoType(PlayerManager.AmmoType.tnt);
                break;
            case WeaponType.EmptyHand:
                PlayerManager.Instance.SetAmmoType(PlayerManager.AmmoType.tnt);
                break;
        }

        PlayerManager.Instance.UpdateAmmoUI();
    }

    public void SetWeaponType(int value) // Determine the animation based on weapon here
    {
        switch (value)
        {
            case 0:
                _weaponType = WeaponType.Melee;
                break;
            case 1:
                _weaponType = WeaponType.Handgun;
                break;
            case 2:
                _weaponType = WeaponType.Magnum;
                break;
            case 3:
                _weaponType = WeaponType.Laser;
                break;
            case 4:
                _weaponType = WeaponType.Shotgun;
                break;
            case 5:
                _weaponType = WeaponType.SubMachineGun;
                break;
            case 6:
                _weaponType = WeaponType.MachineGun;
                break;
            case 7:
                _weaponType = WeaponType.Rifle;
                break;
            case 8:
                _weaponType = WeaponType.RPG;
                break;
            case 9:
                _weaponType = WeaponType.Grenade;
                break;
            case 10:
                _weaponType = WeaponType.Molotov;
                break;
            case 11:
                _weaponType = WeaponType.Mine;
                break;
            case 12:
                _weaponType = WeaponType.TNT;
                break;
            case 13:
                _weaponType = WeaponType.EmptyHand;
                break;
            default:
                _weaponType = WeaponType.Melee;
                break;
        }
    }


    public WeaponType ReturnWeaponType()
    {
        return _weaponType;
    }

    public int GetWeaponTypeIndex()
    {
        return (int)_weaponType;
    }

    public GameObject GetCurrentWeapon()
    {
        if (_weaponIndex >= 0 && _weaponIndex < _weapons.Count)
        {
            return _weapons[_weaponIndex];
        }
        return null;
    }
}
