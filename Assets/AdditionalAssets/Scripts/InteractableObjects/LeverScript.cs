using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverScript : MonoBehaviour
{

    private bool _leverStatus;
    private Animator _anim;
    [SerializeField] private UnityEvent _event;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractWithLever()
    {
        if (_leverStatus == false)
        {
            Debug.Log(_leverStatus);
            _anim.SetBool("LeverStatus", true);
            _event.Invoke();
            _leverStatus = !_leverStatus;
        }

        else
        {
            Debug.Log(_leverStatus);
            _anim.SetBool("LeverStatus", false);
            _leverStatus = !_leverStatus;
        }
    }
}
