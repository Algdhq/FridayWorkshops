using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    private CinemachineImpulseSource _impulse;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: persist across scenes
    }

    // Start is called before the first frame update
    private void Start()
    {
        _impulse = GameObject.Find("PlayerArmature").GetComponent<CinemachineImpulseSource>();
    }

    public void CamShake()
    {
        _impulse.GenerateImpulse();
    }
}
