using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private ParticleSystem _smokeTrail;
    [SerializeField] private GameObject _explosionForce;
    private BoxCollider _boxCollider;
    private MeshRenderer _meshRenderer;
    private GameObject _explosion;

    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _explosion = GameObject.Find("Explosion_09");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (_explosion != null)
        {
            _explosion.transform.position = this.transform.position;
            _explosion.GetComponent<ParticleSystem>().Play();
            PlayerManager.Instance.CamShake();
            AudioManager.Instance.PlaySFXClip(2);
        }
        _speed = 0;
        _boxCollider.enabled = false;
        _meshRenderer.enabled = false;
        _explosionForce.SetActive(true);
        Invoke("TurnOffRocket", 0.1f);
        _smokeTrail.Stop();
    }

    private void TurnOffRocket()
    {
        _explosionForce.SetActive(false);
    }
}
