using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpscareTrigger : MonoBehaviour
{
    [Header("Will the jumpscare pop in front?")]
    [SerializeField] private bool _popInFront;
    [Header("Minimum Distance from player")]
    [SerializeField] private float _minDistance;
    [Header("Maximum Distance from player")]
    [SerializeField] private float _maxDistance;
    [Header("Time to delay jumpscare")]
    [SerializeField] private float _secondsToDeactivate;
    [Header("Spawned Jump Scare/Director here")]
    [SerializeField] private GameObject _jumpScare;
    [Header("Events to run in addition at the start")]
    [SerializeField] private UnityEvent _event;
    [Header("Seconds of delay for delayed events")]
    [SerializeField] private float _secondsToRunSecondEvent;
    [Header("Events to run when delayed")]
    [SerializeField] private UnityEvent _delayedEvent;
    

    private GameObject _player;
    private bool _isVisible;
    private bool _playedJumpscareOnce;

    // Start is called before the first frame update
    private void Start()
    {
        _player = GameObject.Find("PlayerArmature");
        _jumpScare.SetActive(false);
    }

    private void Update()
    {
        if (_popInFront == true)
        {
            float currDistance = Vector3.Distance(transform.position, _player.transform.position);
            if (currDistance > _minDistance && currDistance < _maxDistance)
            {
                if (this.gameObject.GetComponent<Renderer>().isVisible)
                {
                    _popInFront = false;
                    DoJumpScare();
                }
            }
        }
    }

    private void OnBecameVisible()
    {
        float currDistance = Vector3.Distance(transform.position, _player.transform.position);
        if (_popInFront == false)
        {
            if (_playedJumpscareOnce == false)
            {
                if (currDistance > _minDistance && currDistance < _maxDistance)
                {
                    DoJumpScare();
                }
                else
                {
                    HideJumpScare();
                }
            }
        }            
    }


    public void DoJumpScare()
    {
        _isVisible = true;
        _jumpScare.SetActive(true);
        _playedJumpscareOnce = true;
        _event.Invoke();
        if (_secondsToDeactivate > 0)
        {
            StartCoroutine(DeactivateJumpscare());
        }
        if (_secondsToRunSecondEvent > 0)
        {
            StartCoroutine(DelayedSecondEvent());
        }
        Debug.Log(_isVisible);
    }

    public void HideJumpScare()
    {
        _isVisible = false;
        _jumpScare.SetActive(false);
    }

    IEnumerator DeactivateJumpscare()
    {
        yield return new WaitForSeconds(_secondsToDeactivate);
        HideJumpScare();
    }

    IEnumerator DelayedSecondEvent()
    {
        yield return new WaitForSeconds(_secondsToRunSecondEvent);
        _delayedEvent.Invoke();
    }
}
