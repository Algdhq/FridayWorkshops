using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WalkIntoTriggerEvent : MonoBehaviour
{
    [Header("Basic Settings")]
    [SerializeField] private bool _canRunMultipleTimes = false;
    [SerializeField] private bool _delayAtStart = false;

    [Header("Seconds of delay if necessary")]
    [SerializeField] private float _secondsOfDelayAtStart = 0f;

    [Header("Initial Trigger Event")]
    [SerializeField] private UnityEvent _eventAtStart;

    [Header("Additional event after initial")]
    [SerializeField] private float _secondsOfDelayAfterInitialEvent = 0f;
    [SerializeField] private UnityEvent _delayedEvent;

    private bool _hasRun = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (_canRunMultipleTimes || !_hasRun)
        {
            if (_delayAtStart)
                StartCoroutine(PlayDelayTrigger());
            else
                PlayTrigger();

            if (!_canRunMultipleTimes)
                _hasRun = true;
        }
    }

    public void PlayTrigger()
    {
        _eventAtStart.Invoke();

        if (_secondsOfDelayAfterInitialEvent > 0f)
            Invoke(nameof(DelayedEventAfterTrigger), _secondsOfDelayAfterInitialEvent);
    }

    private IEnumerator PlayDelayTrigger()
    {
        yield return new WaitForSeconds(_secondsOfDelayAtStart);
        PlayTrigger();
    }

    private void DelayedEventAfterTrigger()
    {
        _delayedEvent.Invoke();
    }
}
