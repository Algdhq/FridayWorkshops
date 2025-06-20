using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _target;
    [SerializeField] private ParticleSystem _particleBlast;
    [SerializeField] private int _damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 origin = _firePoint.position;
            Vector3 direction = _firePoint.forward;
            Vector3 endpoint = origin + direction * 20f;

            Ray ray = new Ray(origin, direction);
            RaycastHit HitInfo;
            PlayerManager.Instance.CamShake();
            AudioManager.Instance.PlaySFXClip(3);
            
            if (Physics.Raycast(ray, out HitInfo, 20f))
            {
                endpoint = HitInfo.point;
                _target.position = endpoint;

                _lineRenderer.SetPosition(0, _firePoint.position);
                _lineRenderer.SetPosition(1, _target.position);
                _lineRenderer.enabled = true;
                if (HitInfo.collider.GetComponent<HitPoints>() != null)
                {
                    HitInfo.collider.GetComponent<HitPoints>().TakeDamage(_damage);
                }
                _particleBlast.transform.position = endpoint;
                _particleBlast.Play();
                Invoke("TurnOffLaser", 0.2f);
            }

            else
            {
                _lineRenderer.SetPosition(0, _firePoint.position);
                _lineRenderer.SetPosition(1, endpoint);
                _lineRenderer.enabled = true;
                Invoke("TurnOffLaser", 0.2f);
            }


            
        }
    }

    private void TurnOffLaser()
    {
        _lineRenderer.enabled = false;
    }
}
