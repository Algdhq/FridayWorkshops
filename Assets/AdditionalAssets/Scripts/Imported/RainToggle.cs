using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RainToggle : MonoBehaviour
{
    private GameObject rainParticles; // Assign your rain particle system in the inspector
    private bool isInside = false;

    private void Start()
    {
        rainParticles = GameObject.Find("Rain Particle System");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Rain stopped");
            isInside = true;
            CheckBool(); // Stop rain when entering the building
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Rain resumed");
            isInside = false;
            CheckBool(); // Start rain when exiting the building
        }
    }

    private void CheckBool()
    {
        if (isInside == true)
        {
            rainParticles.SetActive(false);
        }
        else
        {
            rainParticles.SetActive(true);
        }
    }
}
