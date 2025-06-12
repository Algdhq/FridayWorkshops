using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private bool _flashlightStatus;
    [SerializeField] private GameObject _lightSource;
    [SerializeField] private StarterAssetsInputs _input;
    private bool _canPress = true;

    void Update()
    {
        if (_input.flashlight && _canPress)
        {
            Debug.Log("Toggle flashlight");
            ToggleFlashlight();
            _canPress = false;
        }

        // Wait for key release to allow next press
        if (!_input.flashlight && !_canPress)
        {
            _canPress = true;
        }
    }

    private void ToggleFlashlight()
    {
        _flashlightStatus = !_flashlightStatus;
        _lightSource.SetActive(_flashlightStatus);
    }
}
