using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemInteractable : MonoBehaviour, Iinteractable
{
    [SerializeField] private bool _canInteractAgain;
    [SerializeField] private UnityEvent _event;
    private bool _hasRun;

    public void RunEvent()
    {        
        if (_hasRun == false)
        {
            if (_canInteractAgain == true)
            {
                _event.Invoke();
            }

            if (_canInteractAgain == false)
            {
                _event.Invoke();
                _hasRun = true;
            }
        }
        else
        {
            return;
        }
    }
}
