using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _flame;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private GameObject _blastForce;
    private MeshRenderer _meshRenderer;
    private HitPoints _hitPoints;
    private bool _startTimer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _hitPoints = GetComponent<HitPoints>();
    }

    public void BarrelLit()
    {
        if (_startTimer) return;
        Debug.Log("Barrel is lit");
        _startTimer = true;
        _flame.SetActive(true);
        StartCoroutine(DecreaseHealth());
    }
    
    public void Explosion()
    {
        _startTimer = false;
        Debug.Log("I shot my barrel");
        _explosion.Play();
        _meshRenderer.enabled = false;
        _flame.SetActive(false);
        AudioManager.Instance.PlaySFXClip(2);
        PlayerManager.Instance.CamShake(PlayerManager.ShakeStrength.Normal);
        _blastForce.SetActive(true);
        Invoke("DestroyMe", 7f);
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }

    IEnumerator DecreaseHealth()
    {
        while(_startTimer == true)
        {
            _hitPoints.TakeDamage(4);
            yield return new WaitForSeconds(1f);
        }        
    }
}
