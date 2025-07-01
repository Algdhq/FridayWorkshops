using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    [SerializeField] private KeySO _keyItem;
    [SerializeField] private int _pickupSFX;

    public void CollectKeyItem()
    {
        InventoryManager.Instance.AddKey(_keyItem);
        AudioManager.Instance.PlaySFXClip(_pickupSFX);
        Destroy(this.gameObject);
        Debug.Log("I picked up the " + _keyItem.itemName);
    }
}
