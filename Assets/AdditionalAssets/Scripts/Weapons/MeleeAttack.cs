using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeAttack : MonoBehaviour
{
    [Header("Enable Section")]
    [SerializeField] private bool _hasEnableSound;
    [SerializeField] private int _enableSound;

    [Header("Swing Section")]
    [SerializeField] private int _swingSound;
    [SerializeField] private bool _hasStrikeSound;
    [SerializeField] private int _strikeSound;

    [Header("Disable Section")]
    [SerializeField] private bool _hasDisableSound;
    [SerializeField] private int _disableSound;

    [Header("Systems")]
    [SerializeField] private float _timer;
    [SerializeField] private UnityEvent _event;

    void OnEnable()
    {
        if (_hasEnableSound == true)
        {
            AudioManager.Instance.PlayWeaponClip(_enableSound);
        }
    }

    public void Swing()
    {
        AudioManager.Instance.PlayWeaponClip(_swingSound);
        StartCoroutine(DelayRaycast());
    }

    IEnumerator DelayRaycast()
    {
        yield return new WaitForSeconds(_timer);
        _event.Invoke();
    }

    private void OnDestroy()
    {
        if (_hasDisableSound == true && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayWeaponClip(_disableSound);
        }
    }

    public void PlayStrikeSound()
    {
        if (_hasStrikeSound == true)
        {
            AudioManager.Instance.PlayWeaponClip(_strikeSound);
            PlayerManager.Instance.CamShake(PlayerManager.ShakeStrength.Weak);
        }
    }
}


