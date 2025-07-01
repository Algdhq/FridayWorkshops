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
    public bool IsAvailable;
    public Sprite image;
    public GameObject thingy;

}
