using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverScript02 : MonoBehaviour
{
    [SerializeField] private bool _leverStatus;
    [SerializeField] private GameObject _leverOn;
    [SerializeField] private GameObject _leverOff;
    [SerializeField] private UnityEvent _event;

    // Start is called before the first frame update
    void Start()
    {
        SetLeverOff();
    }

    public void CheckLeverStatus()
    {
        if (_leverStatus == false)
        {
            SetLeverOn();
        }

        else
        {
            SetLeverOff();
        }
        _leverStatus = !_leverStatus;
    }

    public void SetLeverOn()
    {
        Debug.Log("Turn lever on");
        _leverOn.SetActive(true);
        _leverOff.SetActive(false);
        _event.Invoke();
    }

    public void SetLeverOff()
    {
        Debug.Log("Turn lever off");
        _leverOn.SetActive(false);
        _leverOff.SetActive(true);
    }
}
