using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHandgun : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _target;
    [SerializeField] private ParticleSystem _particleBlast;
    [SerializeField] private int _damage;

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
            RaycastHit hitInfo;
            PlayerManager.Instance.CamShake();
            AudioManager.Instance.PlaySFXClip(3);

            if (Physics.Raycast(ray, out hitInfo, 20f)) // I hit a collider
            {
                endpoint = hitInfo.point;
                _target.position = endpoint;

                Debug.Log(hitInfo.collider.name);
                _lineRenderer.SetPosition(0, _firePoint.position);
                _lineRenderer.SetPosition(1, _target.position);
                _lineRenderer.enabled = true;
                if (hitInfo.collider.GetComponent<HitPoints>() != null)
                {
                    hitInfo.collider.GetComponent<HitPoints>().TakeDamage(_damage);
                }
                _particleBlast.transform.position = endpoint;
                _particleBlast.Play();
                Invoke("TurnOffLaser", 0.2f);
            }

            else // I hit nothing
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
