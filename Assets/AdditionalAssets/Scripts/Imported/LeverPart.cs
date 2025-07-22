using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPart : MonoBehaviour
{
    [SerializeField] private bool _isOn;
    [SerializeField] private GameObject _on;
    [SerializeField] private GameObject _off;
    private LeverMain _leverMain;

    // Start is called before the first frame update
    void Start()
    {
        _leverMain = transform.parent.GetComponent<LeverMain>();
        OnStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeverWasPulled()
    {

        if (_isOn == false)
        {
            _leverMain.AddLeverPulls();
            _isOn = true;
            OnStatus();
        }
    }

    private void OnStatus()
    {
        if (_isOn == false)
        {
            _on.SetActive(false);
            _off.SetActive(true);
        }

        if (_isOn == true)
        {
            _on.SetActive(true);
            _off.SetActive(false);
        }
    }
}
