using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyItemUISlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;

    public void SetKeyItem(KeySO key)
    {
        _icon.sprite = key.icon;        
        _icon.enabled = key.icon != null;
        _name.text = key.itemName;
        _name.enabled = !string.IsNullOrEmpty(key.itemName);

    }

    public void ClearSlot()
    {
        _icon.sprite = null;
        _icon.enabled = false;
        _name.text = "";
        _name.enabled = false;
    }
}
