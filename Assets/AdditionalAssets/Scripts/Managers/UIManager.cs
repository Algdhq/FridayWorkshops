using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public enum AmmoType {Melee, Handgun, Magnum, Laser, Shotgun, SubMachineGun, MachineGun, Rifle, RPG, Grenade, Molotov, Mine, TNT}

    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject _UIMenu;
    [SerializeField] private Button _firstSelectedButton;
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

    public void UpdateStats()
    {
        PlayerStatsSO stats = PlayerManager.Instance.GetStats();
        _healthKitsCollected.text = stats.currentHealthKits.ToString() + "/" + stats.maxHealthKits.ToString();
        _playerName.text = "Name : " + stats.playerName.ToString();
        _currentHealth.text = "Health: " + stats.currentHealth.ToString() + "/" + stats.maxHealth.ToString();
        _expTotal.text = "EXP: " + stats.currentEXP.ToString();
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
}
