using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunEventWhenVisibleTrigger : MonoBehaviour
{
    [Header("Jumpscare to play")]
    [SerializeField] private int _jumpscareNumber;
    [Header("Seconds to delay")]
    [SerializeField] private float _seconds = 0;
    [Header("Event to run")]
    [SerializeField] private UnityEvent _event;

    private bool _wasVisible;
    private bool _hasRun;
    private Animator _GVanim;

    private void Start()
    {
        _GVanim = GameObject.Find("Global Volume_Jumpscare01").GetComponent<Animator>();
    }

    void Update()
    {
        bool isVisible = IsInView(Camera.main);

        if (isVisible && !_wasVisible)
        {
            _wasVisible = true;

            if (_seconds == 0 && _hasRun == false)
            {
                RunEvent();
            }
            else if (_seconds > 0 && _hasRun == false)
            {
                Invoke("RunEvent", _seconds);
            }
            _hasRun = true;
        }
    }

    private void RunEvent()
    {
        _GVanim.SetTrigger("Play");
        AudioManager.Instance.PlayShortJumpscareClip(_jumpscareNumber);
        PlayerManager.Instance.CamShake(PlayerManager.ShakeStrength.Normal);
        _event.Invoke();
    }

    private bool IsInView(Camera cam)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        return GeometryUtility.TestPlanesAABB(planes, GetComponent<Collider>().bounds);
    }
}
