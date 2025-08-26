using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{

    public enum MenuType { Main, Tutorial, VideoAudio, Social }
    [System.Serializable]
    public class MenuPanel
    {
        public MenuType menuType;
        public GameObject panel;
        public Selectable firstSelected; // Optional
    }

    [Header("Menu Elements")]
    [SerializeField] private GameObject _menuRoot; // Always stays active
    [SerializeField] private List<MenuPanel> _menus;
    [SerializeField] private TMP_Text _versionText;

    private MenuType _currentMenu;
    private GameObject _lastSelected;

    private void Start()
    {
        _menuRoot.SetActive(true); // Ensure it's always active
        StartCoroutine(DelayedShowMainMenu());
        if (_versionText != null)
        {
            _versionText.text = $"Build Version: {Application.version}";
        }
    }

    private IEnumerator DelayedShowMainMenu()
    {
        yield return null; // wait one frame so EventSystem is fully ready
        ShowMenu(MenuType.Main);
    }

    private void Update()
    {
        // Play navigation sound when UI selection changes
        if (EventSystem.current.currentSelectedGameObject != _lastSelected)
        {
            _lastSelected = EventSystem.current.currentSelectedGameObject;
            if (_lastSelected != null)
            {
                AudioManager.Instance.PlayUISFXMovement(0);
            }
        }

        // ESC returns to Main menu — nothing closes
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_currentMenu != MenuType.Main)
            {
                ShowMenu(MenuType.Main);
                AudioManager.Instance.PlayUISFXClip(3); // Optional back/cancel sound
            }
        }
    }

    public void ShowMenu(MenuType menuToShow)
    {
        foreach (var menu in _menus)
        {
            bool isTarget = menu.menuType == menuToShow;
            menu.panel.SetActive(isTarget);

            if (isTarget && menu.firstSelected != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(menu.firstSelected.gameObject);
            }
        }

        _currentMenu = menuToShow;
        AudioManager.Instance.PlayUISFXClip(2); // Menu open/select sound
    }

    public void OnMenuButtonClicked(string menuName)
    {
        if (System.Enum.TryParse(menuName, out MenuType parsed))
        {
            ShowMenu(parsed);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
