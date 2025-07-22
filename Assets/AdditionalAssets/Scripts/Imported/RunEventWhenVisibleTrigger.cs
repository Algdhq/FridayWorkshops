using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunEventWhenVisibleTrigger : MonoBehaviour
{
    private bool _isVisible;
    private bool _enableObjectOffScreen;
    private bool _showObject;

    [Header("Play when visible at start")]
    [SerializeField] private bool _playAtStart;
    [Header("Seconds to delay")]
    [SerializeField] private float _seconds = 0;
    [Header("Event to run after delay")]
    [SerializeField] private UnityEvent _delayedEvent;

    private void Start()
    {
        this.GetComponent<MeshRenderer>().enabled = true;
        if(_playAtStart == true)
        {
            runDelayedBool();
        }
    }

    public void StartDirector()
    {
        _isVisible = true;
        Debug.Log("Is visible");
        if (_enableObjectOffScreen == true)
        {
            RunDelayedEvent();
            this.gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {        
        _isVisible = false;
        Debug.Log("Not visible");
        Invoke("runDelayedBool", _seconds);
    }

    private void runDelayedBool()
    {
        _enableObjectOffScreen = true;
    }

    private void RunDelayedEvent()
    {
        _delayedEvent.Invoke();
    }

  
}
