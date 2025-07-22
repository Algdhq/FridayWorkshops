using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverMain : MonoBehaviour
{
    [SerializeField] private LeverPart[] _leverPart;
    [Header("Event that occurs after all levers pulled.")]
    [SerializeField] private UnityEvent _event;

    private int _totalLeverPulls;
    //private ItemPickUpUI _itemPickupUI;


    public void AddLeverPulls()
    {
        _totalLeverPulls ++;
        //_itemPickupUI = GameObject.Find("EnvironmentItemUI").GetComponent<ItemPickUpUI>();
        StringUpdate();
    }
       

    private void StringUpdate()
    {
        int totalPullsLeft = _leverPart.Length - _totalLeverPulls;
        if (totalPullsLeft >= 2)
        {
            //_itemPickupUI.StringUpdate("The lever was pulled.  You have " + totalPullsLeft + " more levers to find.");
        }
        if (totalPullsLeft == 1)
        {
            //_itemPickupUI.StringUpdate("The lever was pulled.  You have 1 lever left to find.");
        }
        if(totalPullsLeft == 0)
        {
            //_itemPickupUI.StringUpdate("The lever was pulled.  You have found all of the levers.");
            _event.Invoke();
        }
    }
}
