using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 500f;
    [SerializeField] private float _radius = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 exlosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(exlosionPos, _radius);

        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(_explosionForce, exlosionPos, _radius, 3.0f);
            }
        }
    }
}
