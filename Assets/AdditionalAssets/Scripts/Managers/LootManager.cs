using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static LootManager Instance { get; private set; }

    [Header("Loot Prefabs")]
    [SerializeField] private GameObject[] _standardLoot;
    [SerializeField] private GameObject[] _epicLoot;
    [SerializeField] private GameObject[] _legendaryLoot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    public GameObject GetRandomStandardLoot()
    {
        if (_standardLoot.Length ==  0)
        {
            return null;
        }
        int index = Random.Range(0, _standardLoot.Length);//if 0-5, this new stand loot
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
        if (_legendaryLoot.Length == 0)
        {
            return null;
        }
        int index = Random.Range(0, _legendaryLoot.Length);
        return _legendaryLoot[index];
    }
}
