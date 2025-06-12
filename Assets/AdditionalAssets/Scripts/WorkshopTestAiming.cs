using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorkshopTestAiming : MonoBehaviour
{

    private Animator _anim;
    private bool _isAiming;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        _isAiming = Input.GetMouseButton(1);
    
        _anim.SetBool("Aiming", _isAiming);

        if (_isAiming == true)
        {
            _anim.SetLayerWeight(1, 1.0f);
        }
        
        else
        {
            _anim.SetLayerWeight(1, 0.0f);
        }
        Debug.Log(_isAiming);
    }
}
