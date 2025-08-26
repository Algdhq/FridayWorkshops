using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.InputSystem;

public class PlayDirectorOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;
    [Header("Features of Cutscene")]
    [SerializeField] private bool _inGameCutscene;
    [SerializeField] private bool _keepGunOnDuringCutscene;
    [SerializeField] private bool _skippableCutscene;
    [SerializeField] private bool _turnOffRaycast;
    [Header("Objects to adjust during cutscene")]
    [SerializeField] private GameObject _inGameReticule;
    [SerializeField] private int _gunModel;

    [Header("This is the immediate event")]
    [SerializeField] private UnityEvent _event;
    [Header("Seconds to play Delayed Event")]
    [SerializeField] private float _time;
    [Header("This is a delayed event")]
    [SerializeField] private UnityEvent _delayedEvent;
    [Header("Event played at end of Timeline Playback")]
    [SerializeField] private UnityEvent _endTimelineEvent;    


    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (_inGameCutscene == true)
            {
                return;
            }
            else
            {
                SkipToEnd();
            }            
        }
    }

    void SkipToEnd()
    {
        if (_director == null) return;
        if (_skippableCutscene == true)
        {
            double duration = _director.duration;
            double skipTime = duration - 0.2f;
            skipTime = Mathf.Max(0, (float)skipTime);
            _director.time = skipTime;
            _director.Play();
            _inGameReticule.SetActive(true);
        }        
    }

    private void OnEnable()
    {
        if (_director == null) return;
        _director.stopped += OnPlayableDirectorStopped;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_director == null) return;
        if (other.CompareTag("Player"))
        {
            if (!_director.gameObject.activeInHierarchy)
            {
                _director.gameObject.SetActive(true);
            }

            if (_inGameReticule != null)
            {           
                if (_inGameCutscene == false)
                {
                    _inGameReticule.SetActive(false);
                    Raycasting.Instance.NoRaycast(true);
                }
            }

            _director.Play();
            _event.Invoke();
            HideGunDuringCutscene();
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            Invoke("DelayedEvent", _time);
        }
    }

    private void DelayedEvent()
    {
        _delayedEvent.Invoke();
    }    

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (_director == aDirector)
        {
            if (_inGameReticule != null)
            {
                _inGameReticule.SetActive(true);
            }
            Raycasting.Instance.NoRaycast(false);
            RevealGunAfterCutscene();
            _endTimelineEvent.Invoke();
            _director.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _director.stopped -= OnPlayableDirectorStopped;
    }

    private void HideGunDuringCutscene()
    {
        if (_keepGunOnDuringCutscene == false)
        {
            _gunModel = InventoryManager.Instance.GetWeaponTypeIndex();
            InventoryManager.Instance.SetCurrentWeapon(13);
        }
        else
        {
            return;
        }
    }

    private void RevealGunAfterCutscene()
    {
        InventoryManager.Instance.SetCurrentWeapon(_gunModel);
    }

    public bool CurrentlyInCutscene()
    {
        return _director != null && _director.state == PlayState.Playing;
    }
}
