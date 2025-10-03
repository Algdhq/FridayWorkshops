using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemVerification : MonoBehaviour
{
    [SerializeField] private string _nameOfItem;
    [SerializeField] private UnityEvent _event;

    public void CheckItem()
    {
        if (InventoryManager02.Instance.UseItem(_nameOfItem))
        {
            Debug.Log("We have the item - do an event");
            _event.Invoke();
        }
        else
        {
            Debug.Log("We don't have the item - don't do the event");
        }
    }
}
