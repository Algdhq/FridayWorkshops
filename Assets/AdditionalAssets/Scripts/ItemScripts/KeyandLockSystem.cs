using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LockState
{
    Locked,
    Unlocking,
    Unlocked
}

public class KeyandLockSystem : MonoBehaviour
{
    [SerializeField] private bool _lockOnStart;
    [SerializeField] private LockState _currentState;
    [SerializeField] private string _nameOfKey;
    [SerializeField] private Animator _anim;
    private BoxCollider _boxCollider;


    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();

        if (_lockOnStart == true)
        {
            _currentState = LockState.Locked;
        }
        else
        {
            _currentState = LockState.Unlocked;
        }
    }


    public void CheckLockState()
    {
        switch(_currentState)
        {
            case LockState.Locked:
                LockedStatus();
                break;
            case LockState.Unlocking:
                UnlockingStatus();
                break;
            case LockState.Unlocked:
                UnlockedStatus();
                break;
        }
    }

    public void LockedStatus()
    {
        if(InventoryManager.Instance.UseKey(_nameOfKey))
        {
            _lockOnStart = false;
            _currentState = LockState.Unlocked;
            UnlockingStatus();
        }
        else
        {
            Debug.Log("Door is Locked");
            AudioManager.Instance.PlaySFXClip(6);
        }        
    }

    public void UnlockingStatus()
    {
        Debug.Log("Door is Unlocking");
        AudioManager.Instance.PlaySFXClip(9);
    }

    public void UnlockedStatus()
    {
        Debug.Log("Door is Unlocked");
        _anim.SetTrigger("OpenDoor");
        _boxCollider.enabled = false;
        AudioManager.Instance.PlaySFXClip(8);
    }
}
