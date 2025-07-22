using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class PlayerNPCDialogue : MonoBehaviour
{
    // Static reference to track the currently active dialogue
    public static PlayerNPCDialogue activeDialogue;

    [Header("These audio clips will play in sequence")]
    [SerializeField] private AudioClip[] audioClips;
    [Header("This is the subtitles in sequence")]
    [SerializeField] private string[] subtitles;
    [Header("This is the space between each clip")]
    [SerializeField] private float delayBetweenClips = 1.0f; // Set the default delay
    [Header("Events when the audio begins")]
    [SerializeField] private UnityEvent _startEvent;
    [Header("Events when dialogue is over")]
    [SerializeField] private UnityEvent _endEvent;

    private TextMeshProUGUI subtitleText; // Reference to TextMeshProUGUI component
    private AudioSource audioSource;
    private int currentClipIndex = 0;
    private bool isPlaying = true;
    private Coroutine audioCoroutine; // Reference to the running coroutine

    private void OnEnable()
    {
        currentClipIndex = 0;

        // Check if there is an active dialogue and stop it
        if (activeDialogue != null && activeDialogue != this)
        {
            activeDialogue.StopDialogue(); // This will stop the audio and disable the GameObject
        }

        // Set this instance as the active dialogue
        activeDialogue = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        subtitleText = GameObject.Find("Text (TMP)_NPCDialogue").GetComponent<TextMeshProUGUI>();
        subtitleText.text = null;

        // Check if there are audio clips assigned
        if (audioClips.Length > 0)
        {
            // Ensure the AudioSource is not null
            if (audioSource == null)
            {
                Debug.LogError("AudioSource component is missing!");
                return;
            }

            // Ensure TextMeshProUGUI component is assigned
            if (subtitleText == null)
            {
                Debug.LogError("TextMeshProUGUI component is missing!");
                return;
            }

            // Start playing the first audio clip
            PlayNextClip();
        }
        else
        {
            Debug.LogWarning("No audio clips assigned to the NPC dialogue.");
        }
    }

    private void PlayNextClip()
    {
        // Ensure currentClipIndex is within the bounds of the audioClips array
        if (currentClipIndex >= 0 && currentClipIndex < audioClips.Length)
        {
            // Set the current clip on the AudioSource
            audioSource.clip = audioClips[currentClipIndex];

            // Set the corresponding subtitle text
            subtitleText.text = subtitles[currentClipIndex];

            // Play the current audio clip
            audioSource.Play();
            if (currentClipIndex == 0)
            {
                StartEvent();
            }

            // Move to the next clip index
            currentClipIndex++;

            // Cancel any previous coroutine and start a new one
            if (audioCoroutine != null)
            {
                StopCoroutine(audioCoroutine); // Stop the previous coroutine
            }

            // Schedule the next clip after the duration of the current clip and the specified delay
            audioCoroutine = StartCoroutine(PlayNextClipAfterDelay());
        }
        else
        {
            Debug.LogError("Invalid currentClipIndex. Ensure the array bounds are correct.");
        }
    }

    private System.Collections.IEnumerator PlayNextClipAfterDelay()
    {
        // Wait for the duration of the current clip and the specified delay
        yield return new WaitForSeconds(audioSource.clip.length + delayBetweenClips);

        // Check if we are still within bounds
        if (currentClipIndex < audioClips.Length)
        {
            PlayNextClip();
        }
        else
        {
            // All clips have been played
            CheckAudioCompletion();
        }
    }

    private void CheckAudioCompletion()
    {
        // Invoke the end event and reset the dialogue
        subtitleText.text = null;
        EndEvent();

        // Disable the GameObject immediately
        gameObject.SetActive(false);

        // Reset activeDialogue when dialogue ends
        activeDialogue = null;
    }

    private void OnDisable()
    {
        // Stop further invocations when the GameObject is disabled
        isPlaying = false;

        // Stop any running coroutine
        if (audioCoroutine != null)
        {
            StopCoroutine(audioCoroutine);
        }
    }

    private void StartEvent()
    {
        _startEvent.Invoke();
    }

    private void EndEvent()
    {
        _endEvent.Invoke();
    }

    public void StopDialogue()
    {
        // Stop the current audio and clear subtitles
        audioSource.Stop();
        subtitleText.text = null;  // Clear subtitle text
        currentClipIndex = 0; // Reset the clip index to start fresh when a new dialogue starts
        isPlaying = false;

        // Call the end event immediately before disabling
        EndEvent();

        // Disable the GameObject to prevent overlap
        gameObject.SetActive(false);

        activeDialogue = null; // Reset the active dialogue
    }
}

