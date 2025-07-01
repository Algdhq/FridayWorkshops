using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [Header("Connected Components")]
    [SerializeField] private PlayerStatsSO _playerStats;
    [SerializeField] private TextMeshProUGUI _healthText;
    
    private CinemachineImpulseSource _impulse;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _impulse = GameObject.Find("PlayerArmature").GetComponent<CinemachineImpulseSource>();
        _healthText.text = "Health: " + _playerStats.currentHealth.ToString();
    }

    public void CamShake()
    {
        _impulse.GenerateImpulse();
    }

    public void UpdateHealthValue(int value)//+20 health
    {
        _playerStats.currentHealth += value;
        _playerStats.currentHealth = Mathf.Clamp(_playerStats.currentHealth, 0, 100);
        _healthText.text = "Health: " + _playerStats.currentHealth.ToString();
        if (_playerStats.currentHealth <= 0)
        {
            Debug.Log("I died - now play death function");
        }
    }
}
