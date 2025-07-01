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
            if (_canInteractAgain)
            {
                _event.Invoke(); // Always allow interaction                
            }
            else if (!_hasRun)
            {
                _event.Invoke(); // Allow once
                _hasRun = true;
            }
        }
        else
        {
            return;
        }
    }

    public void ChangeInteractableAgain()
    {
        _canInteractAgain = !_canInteractAgain;
    }

    public bool CanInteract() => _canInteractAgain;
}
