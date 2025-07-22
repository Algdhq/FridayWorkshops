using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItemScript : MonoBehaviour
{
    public void PickUpHealth()
    {
        Debug.Log("I picked up health" + this.gameObject.name);
        PlayerManager.Instance.AddHealthPack();
        AudioManager.Instance.PlaySFXClip(5);
        Destroy(this.gameObject);
    }
}
