using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    public enum AmmoType { handgun, Shotgun, Rifle}
    [SerializeField] private AmmoType _ammoType;
    [SerializeField] private int _totalAmmo;

    public void PickUpAmmo()
    {
        Debug.Log("This is " + _ammoType);
        if (_ammoType == AmmoType.handgun)
        {
            PlayerManager.Instance.UpdateHandgunAmmo(_totalAmmo);
        }
        if (_ammoType == AmmoType.Shotgun)
        {
            PlayerManager.Instance.UpdateShotgunAmmo(_totalAmmo);
        }
        if(_ammoType == AmmoType.Rifle)
        {
            PlayerManager.Instance.UpdateRifleAmmo(_totalAmmo);
        }
        AudioManager.Instance.PlaySFXClip(15);
        Destroy(this.gameObject);
    }
}
