using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DrawerScript : MonoBehaviour
{
    private bool _drawerStatus;
    [SerializeField] private float _open = 0.5f;
    [SerializeField] private float _close = 0.2f;
    [SerializeField] private float _moveSpeed = 1.0f;
    [SerializeField] private UnityEvent _event;

    private float _targetZ;

    private void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _close);
        _targetZ = _close;
    }


    public void InteractWithDrawer()
    {
        if (_drawerStatus == false)
        {
            Debug.Log(_drawerStatus + "This is open");
            _targetZ = _open;
            _event.Invoke();
            _drawerStatus = !_drawerStatus;
        }

        else
        {
            Debug.Log(_drawerStatus + "This is closed");
            _targetZ = _close;
            _drawerStatus = !_drawerStatus;
        }

        StartCoroutine(MoveDrawer(_targetZ));
    }

    private IEnumerator MoveDrawer(float targetZ)
    {
        while (true)
        {
            Vector3 current = transform.localPosition;
            Vector3 target = new Vector3(current.x, current.y, targetZ);

            transform.localPosition = Vector3.MoveTowards(current, target, Time.deltaTime * _moveSpeed);

            if (Mathf.Abs(transform.localPosition.z - targetZ) < 0.001f)
            {
                transform.localPosition = target;
                break;
            }

            yield return null;
        }
    }
}
