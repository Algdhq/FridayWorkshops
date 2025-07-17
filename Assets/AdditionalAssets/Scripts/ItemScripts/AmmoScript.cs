using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    public enum AmmoType { handgun, shotgun, rifle, magnum, laser, submachinegun, machinegun, rpg, grenade, molotov, mine, tnt }
    [SerializeField] private AmmoType _ammoType;
    [SerializeField] private int _totalAmmo;

    public void PickUpAmmo()
    {
        Debug.Log("This is " + _ammoType);
        if (_ammoType == AmmoType.handgun)
        {
            PlayerManager.Instance.UpdateHandgunAmmo(_totalAmmo);
        }

        if (_ammoType == AmmoType.shotgun)
        {
            PlayerManager.Instance.UpdateShotgunAmmo(_totalAmmo);
        }

        if (_ammoType == AmmoType.rifle)
        {
            PlayerManager.Instance.UpdateRifleAmmo(_totalAmmo);
        }

        if (_ammoType == AmmoType.magnum)
        {
            PlayerManager.Instance.UpdateMagnumAmmo(_totalAmmo);
        }

        if (_ammoType == AmmoType.laser)
        {
            PlayerManager.Instance.UpdateLaserAmmo(_totalAmmo);
        }

        if (_ammoType == AmmoType.submachinegun)
        {
            PlayerManager.Instance.UpdateSubMachineGunAmmo(_totalAmmo);
        }

        if (_ammoType == AmmoType.machinegun)
        {
            PlayerManager.Instance.UpdateMachineGunAmmo(_totalAmmo);
        }

        if (_ammoType == AmmoType.rpg)
        {
            PlayerManager.Instance.UpdateRPGBulletAmmo(_totalAmmo);
        }

        if (_ammoType == AmmoType.grenade)
        {
            PlayerManager.Instance.UpdateGrenadeAmmo(_totalAmmo);
        }

        if (_ammoType == AmmoType.molotov)
        {
            PlayerManager.Instance.UpdateMolotovAmmo(_totalAmmo);
        }

        if (_ammoType == AmmoType.mine)
        {
            PlayerManager.Instance.UpdateMineAmmo(_totalAmmo);
        }

        if (_ammoType == AmmoType.tnt)
        {
            PlayerManager.Instance.UpdateTNTAmmo(_totalAmmo);
        }
        AudioManager.Instance.PlaySFXClip(15);
        PlayerManager.Instance.UpdateAmmoUI();
        Destroy(this.gameObject);
    }
}
