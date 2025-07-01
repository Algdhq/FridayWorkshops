using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItemScript : MonoBehaviour
{
    [SerializeField] private int _healthValue;

    public void PickUpHealth()
    {
        Debug.Log("I picked up health" + this.gameObject.name);
        PlayerManager.Instance.UpdateHealthValue(_healthValue);
        AudioManager.Instance.PlaySFXClip(5);
        Destroy(this.gameObject);
    }
}
