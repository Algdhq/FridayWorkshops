using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveTinaToPositionOnEnable : MonoBehaviour
{
    [SerializeField] private GameObject character; // The character to be moved
    [SerializeField] private GameObject targetLocation; // The location to move the character to
    [Header("Event to play when enabled")]
    [SerializeField] private UnityEvent _event;

    private void OnEnable()
    {
        if (character != null && targetLocation != null)
        {
            // Move the character to the target location's position
            character.transform.position = targetLocation.transform.position;

            // Set the character's rotation to the target location's rotation
            character.transform.rotation = targetLocation.transform.rotation;

            // Move the character to the target location's position
            character.transform.position = targetLocation.transform.position;

            // Set the character's rotation to the target location's rotation
            character.transform.rotation = targetLocation.transform.rotation;

            // Move the character to the target location's position
            character.transform.position = targetLocation.transform.position;

            // Set the character's rotation to the target location's rotation
            character.transform.rotation = targetLocation.transform.rotation;

            // Move the character to the target location's position
            character.transform.position = targetLocation.transform.position;

            // Set the character's rotation to the target location's rotation
            character.transform.rotation = targetLocation.transform.rotation;

            _event.Invoke();
            // Optionally, you can add some debug logs to confirm the movement
            //Debug.Log($"{character.name} has been moved to {targetLocation.name}'s position and rotation.");

            // Disable this script
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Character or targetLocation is not assigned.");
        }
    }
}
