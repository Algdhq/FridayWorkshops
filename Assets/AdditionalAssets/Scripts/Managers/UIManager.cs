using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject _UIMenu;
    [SerializeField] private Button _firstSelectedButton;
    private GameObject _lastSelected;

    private void Awake()
    {
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != _lastSelected)
        {
            _lastSelected = EventSystem.current.currentSelectedGameObject;
            if (_lastSelected != null)
            {
                AudioManager.Instance.PlayUISFXMovement(0);
            }           
        }

        if (Input.GetKeyDown(KeyCode.Tab) && DialogueManager.Instance.CurrentDialogueState == DialogueState.dialogueoff)
        {
            if (_UIMenu.activeSelf)
            {
                CloseInventoryMenu();
            }
            else
            {
                OpenInventoryMenu();
            }
        }        
    }

    public void OpenInventoryMenu()
    {
        GameManager.Instance.PauseGame();
        _UIMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_firstSelectedButton.gameObject);
        AudioManager.Instance.PlayUISFXClip(2);
    }

    public void CloseInventoryMenu()
    {
        _UIMenu.SetActive(false);
        GameManager.Instance.UnPauseGame();
        AudioManager.Instance.PlayUISFXClip(3);
    }
}
