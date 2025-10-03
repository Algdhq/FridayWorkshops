using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCollectableItem", menuName = "Inventory/CollectableItem")]

public class CollectableItemSO : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite icon;
}
