using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SampleWalkIntoTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;
    [SerializeField] private UnityEvent _postEvent;
    //bool runsecondevent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("I hit player");
            _event.Invoke();
        }
    }

    //if (runsecondevent == true)
    //place time
    //coroutine
    //run _postEvent
}
