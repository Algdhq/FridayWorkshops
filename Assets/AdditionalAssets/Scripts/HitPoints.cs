using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitPoints : MonoBehaviour
{
    [SerializeField] private int _hitPoints = 100;
    [SerializeField] private TMP_Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text.text = _hitPoints.ToString();
    }

    public void TakeDamage(int value)
    {
        _hitPoints -= value;
        _text.text = _hitPoints.ToString();
        if (_hitPoints <= 0)
        {
            Debug.Log("I died");
        }
    }
}
