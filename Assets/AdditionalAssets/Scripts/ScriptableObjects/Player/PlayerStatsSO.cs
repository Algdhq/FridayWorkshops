using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/Stats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Health")]
    public int currentHealth = 20;
    public int maxHealth = 100;

    [Header("Mana")]
    public int currentMana = 0;
    public int maxMana = 100;

    [Header("Handgun Ammo")]
    public int currentHandgunAmmo = 0;
    public int handgunClip = 0;
    public int maxHandgunClip = 6;
    public int maxHandgunAmmo = 100;

    [Header("Shotgun Ammo")]
    public int currentShotgunAmmo = 0;
    public int shotgunClip = 0;
    public int maxShotgunClip = 8;
    public int maxShotgunAmmo = 50;

    [Header("Rifle Ammo")]
    public int currentRifleAmmo = 0;
    public int rifleClip = 0;
    public int maxRifleClip = 6;
    public int maxRifleAmmo = 50;

}
