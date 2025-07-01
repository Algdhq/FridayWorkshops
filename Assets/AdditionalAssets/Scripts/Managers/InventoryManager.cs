using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Melee,
    Pistol,
    Rifle
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public List<KeySO> keyItems = new List<KeySO>();
    private int _weaponIndex;
    [SerializeField] WeaponType _weaponType;
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
    }

    public void SetWeaponType(int value)//Determine the animation based on weapon here
    {
        switch(value)
        {
            case 0:
                _weaponType = WeaponType.Melee;
                break;
            case 1:
                _weaponType = WeaponType.Pistol;
                break;
            case 2:
                _weaponType = WeaponType.Pistol;
                break;
            case 3:
                _weaponType = WeaponType.Pistol;
                break;
            case 4:
                _weaponType = WeaponType.Rifle;
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
}
