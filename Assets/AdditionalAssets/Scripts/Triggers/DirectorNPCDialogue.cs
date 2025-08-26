using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DirectorNPCDialogue : MonoBehaviour
{
    [Header("This is the dialogue count")]
    [SerializeField] private int currentClipIndex;

    [Header("This is the subtitles in sequence")]
    [SerializeField] private string[] subtitles;

    [Header("Reference to TextMeshProUGUI component")]
    private TextMeshProUGUI subtitleText; // Reference to TextMeshProUGUI component
    
    private bool isPlaying = true;



    private void PlayNextClip()
    {
        // Check if the currentClipIndex is within the bounds of the audioClips array
        if (isPlaying && currentClipIndex >= 0)
        {            
            // Set the corresponding subtitle text
            subtitleText.text = subtitles[currentClipIndex];

            // Move to the next clip index
            currentClipIndex++;
        }
        else
        {
            Debug.LogError("Invalid currentClipIndex. Ensure the array bounds are correct.");
        }
    }


    private void OnEnable()
    {
        subtitleText = GameObject.Find("Text (TMP)_NPCDialogue").GetComponent<TextMeshProUGUI>();
        subtitleText.text = null;
        PlayNextClip();
    }

    private void OnDisable()
    {
        subtitleText.text = null;
    }
}
