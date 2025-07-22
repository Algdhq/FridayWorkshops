using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _cameras;
    [SerializeField] private int _currentIndex = 0;
    [SerializeField] private bool _inCameraCycle;


    private void Update()
    {
        if (_inCameraCycle == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCameraCycle();
            }
        }        
    }

    public void StartCameraCycle()
    {
        Raycasting.Instance.NoRaycast(true);
        _inCameraCycle = true;
        TurnOffAllCameras();
        
        if (_currentIndex < _cameras.Length)
        {
            _cameras[_currentIndex].SetActive(true);
            _currentIndex++;
        }

        else
        {
            TurnOffAllCameras();
            _inCameraCycle = false;
            _currentIndex = 0;
            Raycasting.Instance.NoRaycast(false);
        }
    }

    public void TurnOffAllCameras()
    {
        foreach (var c in _cameras)
        {
            if (c != null)
                c.SetActive(false);
        }
    }
}
