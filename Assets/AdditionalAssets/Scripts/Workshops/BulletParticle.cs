using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticle : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Hit: " + other.name);

        if (other.GetComponent<HitPoints>() != null)
        {
            other.GetComponent<HitPoints>().TakeDamage(10);
        }        
    }
}
