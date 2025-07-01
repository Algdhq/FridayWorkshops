using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [TextArea(2, 5)]
    public string[] dialogueLines;
    private ItemInteractable _itemInteractable;
    [SerializeField] private UnityEvent _event;

    private void Start()
    {
        _itemInteractable = GetComponent<ItemInteractable>();
    }

    public void StartDialogue()
    {        
        _itemInteractable.CanInteract();
        DialogueManager.Instance.StartDialogue(dialogueLines);
        Invoke("RunUnityEvent", 0.1f);
    }

    public void RunUnityEvent()
    {
        _event.Invoke();
    }
}
