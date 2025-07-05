using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DialogueState { dialogueOn, dialogueoff }

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    private DialogueState _dialogueState;
    public DialogueState CurrentDialogueState => _dialogueState;
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    private string[] _currentLines;
    private int _lineIndex = 0;
    private bool _isActive = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _dialogueUI.SetActive(false);
        _dialogueText.text = "";
        _dialogueState = DialogueState.dialogueoff;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _isActive == true)
        {
            _lineIndex++;
            if (_lineIndex < _currentLines.Length)
            {
                _dialogueText.text = _currentLines[_lineIndex];
            }
            else
            {
                EndDialogue();
            }
        }
    }

    public void StartDialogue(string[] lines)
    {
        _dialogueState = DialogueState.dialogueOn;
        Raycasting.Instance.enabled = false;
        GameManager.Instance.PauseGame();
        _dialogueUI.SetActive(true);
        _currentLines = lines;
        _lineIndex = 0;
        _isActive = true;
        _dialogueText.text = _currentLines[_lineIndex];
    }

    public void EndDialogue()
    {
        GameManager.Instance.UnPauseGame();
        _isActive = false;
        _dialogueUI.SetActive(false);
        Raycasting.Instance.enabled = true;
        _dialogueState = DialogueState.dialogueoff;
    }
}



