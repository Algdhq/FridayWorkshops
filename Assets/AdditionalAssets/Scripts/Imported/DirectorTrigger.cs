using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class DirectorTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _director.Play();
        }
    }
}
