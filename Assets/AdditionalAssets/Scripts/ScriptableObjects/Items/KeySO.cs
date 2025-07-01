using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewKeyItem", menuName ="Inventory/KeyItem")]

public class KeySO : ScriptableObject
{
    public string itemName;
    [TextArea(2, 5)]
    public string itemDescription;
    public Sprite icon;
}
