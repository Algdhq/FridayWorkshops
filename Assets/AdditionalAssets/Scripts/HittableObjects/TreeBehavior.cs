using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehavior : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject _woodCollectable;
    private MeshRenderer _meshRenderer;
    private BoxCollider _boxCollider;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void GotHit()
    {
        AudioManager.Instance.PlaySFXClip(18);
    }

    public void ChopDownTree()
    {
        _anim.SetTrigger("TreeFall");
        AudioManager.Instance.PlaySFXClip(19);
        PlayerManager.Instance.CamShake(PlayerManager.ShakeStrength.Weak);
        StartCoroutine(TreeHitsGround());
        _boxCollider.enabled = false;
    }

    public void SpawnItem()
    {
        Instantiate(_woodCollectable, transform.position, Quaternion.Euler(90,0,0));
    }

    public void HideTree()
    {
        _meshRenderer.enabled = false;
    }

    IEnumerator TreeHitsGround()
    {
        yield return new WaitForSeconds(3.0f);
        PlayerManager.Instance.CamShake(PlayerManager.ShakeStrength.Normal);
        AudioManager.Instance.PlaySFXClip(20);
        SpawnItem();
        Invoke("HideTree", 2.0f);
        Destroy(this.gameObject, 5.0f);
    }
}
