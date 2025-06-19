using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static LootManager Instance { get; private set; }

    [Header("Loot Prefabs")]
    [SerializeField] private GameObject[] _standardLoot;
    [SerializeField] private GameObject[] _epicLoot;
    [SerializeField] private GameObject[] _LegendaryLoot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject GetRandomStandardLoot()
    {
        if (_standardLoot.Length == 0)
        {
            return null;
        }
        int index = Random.Range(0, _standardLoot.Length);
        return _standardLoot[index];
    }

    public GameObject GetRandomEpicLoot()
    {
        if (_epicLoot.Length == 0)
        {
            return null;
        }
        int index = Random.Range(0, _epicLoot.Length);
        return _epicLoot[index];
    }

    public GameObject GetRandomLegendaryLoot()
    {
        if (_LegendaryLoot.Length == 0)
        {
            return null;
        }
        int index = Random.Range(0, _LegendaryLoot.Length);
        return _LegendaryLoot[index];
    }
}
