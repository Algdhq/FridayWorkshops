using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DoorScript : MonoBehaviour
{
    private bool _doorStatus;                        // false = closed, true = open
    [SerializeField] private float _openAngle = 90f; // local Y when open
    [SerializeField] private float _closeAngle = 0f; // local Y when closed
    [SerializeField] private float _rotateSpeed = 180f; // degrees per second
    [SerializeField] private UnityEvent _event;

    // Optional SFX IDs to mirror your drawer
    [SerializeField] private int _openSfxId = 14;
    [SerializeField] private int _closeSfxId = 13;

    private float _targetY;

    private void Start()
    {
        // Start closed
        Vector3 e = transform.localEulerAngles;
        e.y = _closeAngle;
        transform.localEulerAngles = e;
        _targetY = _closeAngle;
    }

    public void InteractWithDoor()
    {
        if (_doorStatus == false)
        {
            _targetY = _openAngle;
            AudioManager.Instance.PlaySFXClip(_openSfxId);
            _event.Invoke();
            _doorStatus = !_doorStatus;
        }
        else
        {
            _targetY = _closeAngle;
            AudioManager.Instance.PlaySFXClip(_closeSfxId);
            _doorStatus = !_doorStatus;
        }

        StartCoroutine(RotateDoor(_targetY));
    }

    private IEnumerator RotateDoor(float targetY)
    {
        while (true)
        {
            float currentY = transform.localEulerAngles.y;

            // Step toward target using proper angle wrap-around
            float nextY = Mathf.MoveTowardsAngle(currentY, targetY, _rotateSpeed * Time.deltaTime);

            Vector3 e = transform.localEulerAngles;
            e.y = nextY;
            transform.localEulerAngles = e;

            // Close enough?
            if (Mathf.Abs(Mathf.DeltaAngle(nextY, targetY)) < 0.1f)
            {
                e.y = targetY;
                transform.localEulerAngles = e;
                break;
            }

            yield return null;
        }
    }
}
