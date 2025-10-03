using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiomanagerTest : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("U key pressed");
            AudioManager02.Instance.PlayUISFXClip(0);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("U key pressed");
            AudioManager02.Instance.PlayUISFXClip(1);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("U key pressed");
            AudioManager02.Instance.PlayUISFXClip(2);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("U key pressed");
            AudioManager02.Instance.PlayUISFXClip(3);
        }
        //if (player.takedamage) --> do this
        //If (Player.FireGun) --> Do this
    }
}
