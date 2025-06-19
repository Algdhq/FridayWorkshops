using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerWalkInto : MonoBehaviour
{
    private bool _hasRun;
    [SerializeField] private bool _canRunMultipleTimes;
    [SerializeField] private UnityEvent _event;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_canRunMultipleTimes == true)
            {
                Debug.Log("I ran into the player");
                _event.Invoke();
            }
            
            if (_canRunMultipleTimes == false)
            {
                if (_hasRun == false)
                {
                    Debug.Log("I ran into the player");
                    _event.Invoke();
                    _hasRun = true;
                }
            }
        }
    }
}
