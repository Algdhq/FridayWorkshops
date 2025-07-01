using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isPaused;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

    }
    // Start is called before the first frame update
    
    public void PauseGame()
    {
        Time.timeScale = 0;
        FindObjectOfType<StarterAssetsInputs>().isPaused = true;

    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        FindObjectOfType<StarterAssetsInputs>().isPaused = false;
    }
}
