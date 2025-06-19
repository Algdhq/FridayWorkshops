using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class RPGFiring : MonoBehaviour
{
    [SerializeField] private GameObject _rocket;
    [SerializeField] private GameObject _rocketPosition;
    [SerializeField] private GameObject _rocketProp;
    [SerializeField] private Transform _playerArmature;
    [SerializeField] private StarterAssetsInputs _input;
    private bool _canFire;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.shoot)
        {
            Debug.Log("Fire Missile");
            Instantiate(_rocket, _rocketPosition.transform.position, _rocketPosition.transform.rotation);
            PlayerManager.Instance.CamShake();
            AudioManager.Instance.PlaySFXClip(1);
            _rocketProp.SetActive(false);

            _input.shoot = false; // manually reset it
        }
    }
}
