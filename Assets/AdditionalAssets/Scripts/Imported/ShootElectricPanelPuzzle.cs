using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootElectricPanelPuzzle : MonoBehaviour
{
    [Header("Place all panels in here")]
    [SerializeField] private ShootElectricPanels[] _electricPanels;
    [Header("Event that occurs after solving")]
    [SerializeField] private UnityEvent _events;
    private int _totalBoolsTrue = 0;
    
    public void checkPanelStatus()
    {
        foreach(var e in _electricPanels)
        {
            if (e._isPanelBroken == true)
            {
                _totalBoolsTrue++;                
            }
        }

        ValidatePuzzle();
    }

    private void ValidatePuzzle()
    {
        if (_totalBoolsTrue == _electricPanels.Length)
        {
            _events.Invoke();
        }
        else
        {
            _totalBoolsTrue = 0;
        }
    }
}
