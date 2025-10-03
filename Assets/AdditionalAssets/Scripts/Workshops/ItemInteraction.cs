using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private bool _canInteractAgain;
    [SerializeField] private UnityEvent _event;
    private bool _hasRun = false;

    public void OnInteract()
    {
        if(_canInteractAgain)
        {
            Debug.Log("I'm interacting with this item");
            _event.Invoke();
        }    

        else if (_hasRun == false)
        {
            //allow myself to run script once
            Debug.Log("I'm interacting with this item");
            _event.Invoke();
            _hasRun = true;
        }
    }
}
