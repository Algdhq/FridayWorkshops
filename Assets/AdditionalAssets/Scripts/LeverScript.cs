using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverScript : MonoBehaviour
{
    [SerializeField] private Transform _lever;
    [SerializeField] private UnityEvent _event;
    private bool _leverStatus;
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void InteractWithLever()
    {
        if (_leverStatus == false)
        {
            Debug.Log(_leverStatus);
            _anim.SetBool("LeverStatus", false);
            _event.Invoke();
            _leverStatus = !_leverStatus;
        }
        else
        {
            Debug.Log(_leverStatus);
            _anim.SetBool("LeverStatus", true);
            _leverStatus = !_leverStatus;
        }
    }
}
