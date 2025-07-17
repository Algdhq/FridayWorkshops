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
        UpdateAmmoCounts();
    }

    public void CloseInventoryMenu()
    {
        _UIMenu.SetActive(false);
        GameManager.Instance.UnPauseGame();
        AudioManager.Instance.PlayUISFXClip(3);
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
