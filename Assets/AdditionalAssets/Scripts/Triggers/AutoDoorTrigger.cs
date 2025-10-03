using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoorTrigger : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Open door");
            _anim.SetBool("OpenDoor", true);
            AudioManager02.Instance.PlayUISFXClip(4);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Close Door");
            _anim.SetBool("OpenDoor", false);
            AudioManager02.Instance.PlayUISFXClip(5);
        }
    }
}
