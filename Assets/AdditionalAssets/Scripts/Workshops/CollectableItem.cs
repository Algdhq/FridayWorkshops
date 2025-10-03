using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private CollectableItemSO _collectableItem;

    public void CollectItem()
    {
        InventoryManager02.Instance.AddItem(_collectableItem);
        Destroy(this.gameObject);
        Debug.Log("I picked up the " + _collectableItem.itemName);
    }
}
