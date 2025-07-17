using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastingWeapon : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private float _range = 100f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private LayerMask _hitLayers;

    private GameObject _bloodParticlePosition;
    private ParticleSystem _bloodParticles;
    private MeleeAttack _meleeAttack;

    private void Start()
    {
        _bloodParticlePosition = GameObject.Find("BloodBurstPosition_DoNotRemove");
        _bloodParticles = _bloodParticlePosition.GetComponentInChildren<ParticleSystem>();
        _meleeAttack = GetComponent<MeleeAttack>();
    }

    public void RaycastWeapon()
    {
        Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _range, ~_hitLayers))
        {
            //Debug.Log(hit.collider.name);
            HitPoints hp = hit.collider.GetComponent<HitPoints>();
            if (_meleeAttack != null)
            {
                _meleeAttack.PlayStrikeSound();
            }

            if (hp != null)
            {
                hp.TakeDamage(_damage);
                if (hit.collider.CompareTag("Enemy"))
                {
                    _bloodParticlePosition.transform.position = hit.point;
                    _bloodParticles.Play();
                }
            }            
        }
    }
}
