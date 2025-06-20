using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxScript : MonoBehaviour
{
    [SerializeField] private Transform _lid;
    [SerializeField] private Vector3 _closed;
    [SerializeField] private Vector3 _open;
    [SerializeField] private Transform _lootSpawnPosition;
    private bool _isOpened;

    private Quaternion _closedRotation;
    private Quaternion _openRotation;

    // Start is called before the first frame update
    void Start()
    {
        _closedRotation = Quaternion.Euler(_closed);
        _openRotation = Quaternion.Euler(_open);
        _lid.localRotation = _closedRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isOpened == true)
        {
            _lid.localRotation = Quaternion.RotateTowards(_lid.localRotation, _openRotation, Time.deltaTime * 90f);
        }
    }

    public void OpenChest()
    {
        if (_isOpened == false)
        {
            _isOpened = true;
            RunLoot();
            Invoke("StopOpening", 2.0f);
        }
    }

    private void StopOpening()
    {
        _isOpened = false;
    }

    private void RunLoot()
    {        
        float Roll = Random.Range(0.0f, 1.0f);
        Debug.Log("My roll is " + Roll);
        if (Roll > 0.8f)
        {
            Debug.Log("I rolled Legendary");
            GameObject Loot = LootManager.Instance.GetRandomLegendaryLoot();
            Instantiate(Loot, _lootSpawnPosition.position, _lootSpawnPosition.rotation);
            AudioManager.Instance.PlayUISFXClip(0);
        }
        else if (Roll < 0.2f)
        {
            Debug.Log("I rolled Epic");
            GameObject Loot = LootManager.Instance.GetRandomEpicLoot();
            Instantiate(Loot, _lootSpawnPosition.position, _lootSpawnPosition.rotation);
            AudioManager.Instance.PlayUISFXClip(0);
        }
        else
        {
            Debug.Log("I rolled standard");
            GameObject Loot = LootManager.Instance.GetRandomStandardLoot();
            Instantiate(Loot, _lootSpawnPosition.position, _lootSpawnPosition.rotation);
            AudioManager.Instance.PlayUISFXClip(0);
        }
    }
}
