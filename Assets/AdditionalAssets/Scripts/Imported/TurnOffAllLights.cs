using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnOffAllLights : MonoBehaviour
{
    public enum LightStatus
    {
        LightsOn,
        LightsOff,
        AllowLightsOn
    }

    public LightStatus lightstatus;

    [Header("Events to run when the lights go out")]
    [SerializeField] private UnityEvent _events;

    public void CheckLightStatus()
    {
        if (lightstatus == LightStatus.LightsOn)
        {
            TurnAllLightsOn();
        }

        if (lightstatus == LightStatus.LightsOff)
        {
            TurnAllLightsOff();
        }

        if (lightstatus == LightStatus.AllowLightsOn)
        {
            AllowLightsToBeTurnedOn();
        }
    }
 

    public void TurnAllLightsOff()
    {
        LightSwitch[] components = FindObjectsOfType<LightSwitch>();
        for (int i = 0; i < components.Length; i++)
        {            
            components[i].ManuallyAdjustLightBool(true);            
            components[i].CheckLightStatus();
            components[i].ManuallyAdjustKeepLightsOff(true);
        }
        _events.Invoke();

    }

    public void TurnAllLightsOn()
    {
        LightSwitch[] components = FindObjectsOfType<LightSwitch>();
        for (int i = 0; i < components.Length; i++)
        {
            components[i].ManuallyAdjustKeepLightsOff(false);
            components[i].ManuallyAdjustLightBool(false);
            components[i].CheckLightStatus();
        }
        _events.Invoke();

    }

    public void AllowLightsToBeTurnedOn()
    {
        LightSwitch[] components = FindObjectsOfType<LightSwitch>();
        for (int i = 0; i < components.Length; i++)
        {
            components[i].ManuallyAdjustKeepLightsOff(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckLightStatus();
            this.gameObject.SetActive(false);
        }
    }
}