using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraSystems : MonoBehaviour
{

    public void PlayFootstepAudio(int value)
    {
        AudioManager.Instance.PlayFootstepClip(value);
    }
}
