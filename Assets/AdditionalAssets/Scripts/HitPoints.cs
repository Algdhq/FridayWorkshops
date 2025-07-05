using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class HitPoints : MonoBehaviour
{

    [SerializeField] private int _hitPoints = 100;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private UnityEvent _hitEvent;
    [SerializeField] private UnityEvent _deathEvent;
    private bool _isDead;

  
    // Start is called before the first frame update
    void Start()
    {
        if (_text != null)
        {
            _text.text = _hitPoints.ToString();
        }
    }

    public void TakeDamage(int value)
    {
        if (_isDead) return;

        _hitPoints -= value;

        if (_text != null)
        {
            _text.text = _hitPoints.ToString();
        }

        if (_hitPoints > 0)
        {
            _hitEvent.Invoke();
        }

        if (_hitPoints <= 0)
        {
            _isDead = true;
            _deathEvent.Invoke();
        }
    }
}
