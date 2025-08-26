using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public enum AmmoType {Melee, Handgun, Magnum, Laser, Shotgun, SubMachineGun, MachineGun, Rifle, RPG, Grenade, Molotov, Mine, TNT}
    public enum MenuType {Main, Tutorial, Costume, VideoAudio, Restart, Social }
    [System.Serializable]
    public class MenuPanel
    {
        public MenuType menuType;
        public GameObject panel;
        public Selectable firstSelected; // Optional
    }

    public static UIManager Instance { get; private set; }
    [Header("Menu Elements")]
    [SerializeField] private GameObject _UIMenu;
    [SerializeField] private Button _firstSelectedButton;

    [SerializeField] private GameObject _GameOver;
    [SerializeField] private Button _firstSelectedButtonGameOver;

    [SerializeField] private GameObject _menuRoot;
    [SerializeField] private List<MenuPanel> _menus;
    [SerializeField] private TMP_Text _versionText;
    private MenuType _currentMenu;


    [Header("Inventory Menu Elements")]
    [SerializeField] private TextMeshProUGUI[] _ammoCounts;
    [SerializeField] private TextMeshProUGUI _mission;
    [SerializeField] private KeyItemUISlot[] _keyItemSlots;
    [SerializeField] private TextMeshProUGUI _healthKitsCollected;
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private TextMeshProUGUI _currentHealth;
    [SerializeField] private TextMeshProUGUI _expTotal;
    [SerializeField] private TextMeshProUGUI _totalCoins;
    [SerializeField] private TextMeshProUGUI _playerLevel;
    [SerializeField] private TextMeshProUGUI _playerStatus;
    private GameObject _lastSelected;
    private bool _isPauseMenuOpen;
    private bool _isGameOverMenuOpen;


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

    private void Start()
    {
        _UIMenu.SetActive(false);
        _menuRoot.SetActive(false);
        _currentMenu = MenuType.Main;
        if (_versionText != null)
        {
            _versionText.text = $"Build Version: {Application.version}";
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

        if (Input.GetKeyDown(KeyCode.Tab) && DialogueManager.Instance.CurrentDialogueState == DialogueState.dialogueoff && _isGameOverMenuOpen == false)
        {
            if (IsAnyCutscenePlaying() || (_menuRoot != null && _menuRoot.activeSelf))
            {
                return;
            }

            if (_UIMenu.activeSelf)
            {
                CloseInventoryMenu();
            }
            else
            {
                OpenInventoryMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && DialogueManager.Instance.CurrentDialogueState == DialogueState.dialogueoff && _isGameOverMenuOpen == false)
        {
            if (IsAnyCutscenePlaying() || IsInventoryOpen) return;

            if (_menuRoot != null && _menuRoot.activeSelf)
            {
                HideAllMenus();
                GameManager.Instance.UnPauseGame();
            }
            else
            {
                ShowMenu(MenuType.Main);
                GameManager.Instance.PauseGame();
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
        UpdateAmmoCounts();
        UpdateStats();
        UpdateKeyItemDisplay();
    }

    public void CloseInventoryMenu()
    {
        bool wasOpen = _UIMenu.activeInHierarchy;

        _UIMenu.SetActive(false);
        GameManager.Instance.UnPauseGame();

        if (wasOpen) // Only play SFX if the menu was actually open
        {
            AudioManager.Instance.PlayUISFXClip(3);
        }
    }

    public bool IsInventoryOpen => _UIMenu.activeSelf;

    public void ShowMenu(MenuType menuToShow)
    {
        if (_menuRoot != null && !_menuRoot.activeSelf)
            _menuRoot.SetActive(true);

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
        AudioManager.Instance.PlayUISFXClip(6);

    }

    public void HideAllMenus()
    {
        foreach (var menu in _menus)
            menu.panel.SetActive(false);

        if (_menuRoot != null)
            _menuRoot.SetActive(false);

        AudioManager.Instance.PlayUISFXClip(7);
    }

    public void OnMenuButtonClicked(string menuName)
    {
        if (System.Enum.TryParse(menuName, out MenuType parsed))
        {
            ShowMenu(parsed);
        }
    }

    public void ChooseCostume(int costumeValue)
    {
        Debug.Log("Costume Picked is " + costumeValue);
    }

    public bool IsAnyCutscenePlaying()
    {
        PlayDirectorOnTriggerEnter[] allDirectors = GameObject.FindObjectsOfType<PlayDirectorOnTriggerEnter>();

        foreach (var dir in allDirectors)
        {
            if (dir != null && dir.CurrentlyInCutscene())
            {
                return true;
            }
        }

        return false;
    }

    public void UpdateStats()
    {
        PlayerStatsSO stats = PlayerManager.Instance.GetStats();
        _healthKitsCollected.text = stats.currentHealthKits.ToString() + "/" + stats.maxHealthKits.ToString();
        _playerName.text = "Name : " + stats.playerName.ToString();
        _currentHealth.text = "Health: " + stats.currentHealth.ToString() + "/" + stats.maxHealth.ToString();
        _expTotal.text = "EXP: " + stats.currentEXP.ToString() + " / " + stats._levelThreshold[stats.currentLevel-1].requiredEXP;
        _totalCoins.text = "Coins: " + stats.totalCoins.ToString();
        _playerLevel.text = "Level: " + stats.currentLevel.ToString();
        UpdatePlayerStatus();
        _mission.text = stats.currentMission.ToString();
    }

    public void UpdatePlayerStatus()
    {
        PlayerStatsSO stats = PlayerManager.Instance.GetStats();
        if (stats.currentHealth > 70)
        {
            stats.characterStatus = "Player Status: I think I'm doing OK.  I need to be careful though.";
        }
        else if (stats.currentHealth < 69 && stats.currentHealth > 30)
        {
            stats.characterStatus = "Player Status: I'm really pretty bad.  I need to patch up.  I need to use a med kit.";
        }
        else if (stats.currentHealth < 29 && stats.currentHealth > 10)
        {
            stats.characterStatus = "Player Status: I think I'm gonna die...  I need a med kit - FAST!";
        }
        else if (stats.currentHealth < 9)
        {
            stats.characterStatus = "Player Status: This is it!  I'm going to die if I don't get a health kit right now!";
        }
        _playerStatus.text = stats.characterStatus.ToString();
    }

    public void UpdateKeyItemDisplay()
    {
        var keyItems = InventoryManager.Instance.keyItems;

        for (int i = 0; i < _keyItemSlots.Length; i++)
        {
            if (i < keyItems.Count)
            {
                _keyItemSlots[i].SetKeyItem(keyItems[i]);
            }
            else
            {
                _keyItemSlots[i].ClearSlot();
            }
        }
    }

    public void UpdateAmmoCounts()
    {
        PlayerStatsSO stats = PlayerManager.Instance.GetStats();

        _ammoCounts[0].text = "∞"; // melee
        _ammoCounts[1].text = stats.currentHandgunAmmo.ToString();
        _ammoCounts[2].text = stats.currentMagnumAmmo.ToString();
        _ammoCounts[3].text = stats.currentLaserAmmo.ToString();
        _ammoCounts[4].text = stats.currentShotgunAmmo.ToString();
        _ammoCounts[5].text = stats.currentSubMachineGunAmmo.ToString();
        _ammoCounts[6].text = stats.currentMachineGunAmmo.ToString();
        _ammoCounts[7].text = stats.currentRifleAmmo.ToString();
        _ammoCounts[8].text = stats.currentRPGAmmo.ToString();
        _ammoCounts[9].text = stats.currentGrenadeAmmo.ToString();
        _ammoCounts[10].text = stats.currentMolotovAmmo.ToString();
        _ammoCounts[11].text = stats.currentMineAmmo.ToString();
        _ammoCounts[12].text = stats.currentTNTAmmo.ToString();
    }

    public void OpenGameOverScreen()
    {
        _isGameOverMenuOpen = true;
        AudioManager.Instance.PlayUISFXClip(8);
        _GameOver.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_firstSelectedButtonGameOver.gameObject);              
    }

    public void UpdateGameOverScreen(bool value)
    {
        _isGameOverMenuOpen = value;
    }
}
