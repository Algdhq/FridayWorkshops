using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager02 : MonoBehaviour
{
    public static InventoryManager02 Instance { get; private set; }
    public List<CollectableItemSO> collectableItem = new List<CollectableItemSO>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddItem(CollectableItemSO value)
    {
        if (!collectableItem.Contains(value))
        {
            collectableItem.Add(value);
        }
    }

    public bool UseItem(string value)
    {
        foreach(CollectableItemSO c in collectableItem)
        {
            if (c.itemName == value)
            {
                collectableItem.Remove(c);
                return true;
            }
        }
        return false;
    }
}
