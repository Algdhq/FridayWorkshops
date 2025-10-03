using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingPropaneTank : MonoBehaviour
{

    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private GameObject _explosionForce;


    private MeshRenderer _meshRenderer;
    private BoxCollider _collider;

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<BoxCollider>();
    }



    public void Explosion()
    {
        _explosion.Play();
        PlayerManager.Instance.CamShake(PlayerManager.ShakeStrength.Normal);
        AudioManager.Instance.PlaySFXClip(2);
        _explosionForce.SetActive(true);
        StartCoroutine(DestroyPropaneTank());
    }

    private IEnumerator DestroyPropaneTank()
    {
        _meshRenderer.enabled = false;
        _collider.enabled = false;
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
}
