using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelThreshold 
{ 
    public int level; 
    public int requiredEXP; 
}

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/Stats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Health")]
    public int currentHealth = 20;
    public int maxHealth = 100;

    [Header("Name")]
    public string playerName;

    [Header("EXP")]
    public int currentEXP = 0;
    public int currentLevel = 1;
    public List<LevelThreshold> _levelThreshold = new List<LevelThreshold>();

    [Header("Coins")]
    public int totalCoins;

    [Header("Status")]
    public string characterStatus;

    [Header("Mission")]
    public string currentMission;

    [Header("HealthKits")]
    public int currentHealthKits = 0;
    public int maxHealthKits = 5;
    public int healthPointsRestored = 75;

    [Header("Handgun Ammo")]
    public int currentHandgunAmmo = 0;
    public int handgunClip = 0;
    public int maxHandgunClip = 15;
    public int maxHandgunAmmo = 100;

    [Header("Magnum Ammo")]
    public int currentMagnumAmmo = 0;
    public int MagnumClip = 0;
    public int maxMagnumClip = 6;
    public int maxMagnumAmmo = 100;

    [Header("Laser Ammo")]
    public int currentLaserAmmo = 0;
    public int LaserClip = 0;
    public int maxLaserClip = 6;
    public int maxLaserAmmo = 100;

    [Header("Shotgun Ammo")]
    public int currentShotgunAmmo = 0;
    public int shotgunClip = 0;
    public int maxShotgunClip = 8;
    public int maxShotgunAmmo = 50;

    [Header("SubMachineGun Ammo")]
    public int currentSubMachineGunAmmo = 0;
    public int SubMachineGunClip = 0;
    public int maxSubMachineGunClip = 30;
    public int maxSubMachineGunAmmo = 100;

    [Header("MachineGun Ammo")]
    public int currentMachineGunAmmo = 0;
    public int MachineGunClip = 0;
    public int maxMachineGunClip = 30;
    public int maxMachineGunAmmo = 100;

    [Header("Rifle Ammo")]
    public int currentRifleAmmo = 0;
    public int rifleClip = 0;
    public int maxRifleClip = 6;
    public int maxRifleAmmo = 50;

    [Header("RPG Ammo")]
    public int currentRPGAmmo = 0;
    public int RPGClip = 0;
    public int maxRPGClip = 1;
    public int maxRPGAmmo = 6;

    [Header("Grenade Ammo")]
    public int currentGrenadeAmmo = 0;
    public int GrenadeClip = 0;
    public int maxGrenadeClip = 1;
    public int maxGrenadeAmmo = 6;

    [Header("Molotov Ammo")]
    public int currentMolotovAmmo = 0;
    public int MolotovClip = 0;
    public int maxMolotovClip = 1;
    public int maxMolotovAmmo = 6;

    [Header("Mine Ammo")]
    public int currentMineAmmo = 0;
    public int MineClip = 0;
    public int maxMineClip = 1;
    public int maxMineAmmo = 6;

    [Header("TNT Ammo")]
    public int currentTNTAmmo = 0;
    public int TNTClip = 0;
    public int maxTNTClip = 1;
    public int maxTNTAmmo = 6;

    [Header("EmptyHand Ammo")]
    public int currentEmptyHandAmmo = 0;
    public int EmptyHandClip = 0;
    public int maxEmptyHandClip = 6;
    public int maxEmptyHandAmmo = 100;
}
