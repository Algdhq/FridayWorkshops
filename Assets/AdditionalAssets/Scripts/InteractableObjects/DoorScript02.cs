using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript02 : MonoBehaviour
{
    private bool _doorStatus;
    [SerializeField] private float _closeAngle = 0f;
    [SerializeField] private float _openAngle = 90f;
    [SerializeField] private float _rotationSpeed = 180f;

    [Header("SFX")]
    [SerializeField] private int _openSFX = 21;
    [SerializeField] private int _closeSFX = 22;

    private float _targetY;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 e = transform.localEulerAngles;//0
        e.y = _closeAngle;//0
        transform.localEulerAngles = e;
        _targetY = _closeAngle;
    }

    public void InteractWithDoor()
    {
        if (_doorStatus == false)
        {
            _targetY = _openAngle;
            AudioManager.Instance.PlaySFXClip(_openSFX);
            _doorStatus = !_doorStatus;
        }
        else
        {
            _targetY = _closeAngle;
            AudioManager.Instance.PlaySFXClip(_closeSFX);
            _doorStatus = !_doorStatus;
        }
        StartCoroutine(RotateDoor(_targetY));
    }

    private IEnumerator RotateDoor(float targetY)
    {
        while (true)
        {
            float currentY = transform.localEulerAngles.y;

            float nextY = Mathf.MoveTowardsAngle(currentY, targetY, Time.deltaTime * _rotationSpeed);
            Vector3 e = transform.localEulerAngles;
            e.y = nextY;
            transform.localEulerAngles = e;

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
