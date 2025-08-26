using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerTrigger : MonoBehaviour
{
    [Header("Be sure to set to trigger | Ignore Raycast Weapon")]
    [SerializeField] private int _damageGiven;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerManager.Instance.CooldownStatus())
            {
                int damage = _damageGiven * -1;
                PlayerManager.Instance.UpdateHealthValue(damage);
                PlayerManager.Instance.StartCooldown();
            }            
        }
    }
}
