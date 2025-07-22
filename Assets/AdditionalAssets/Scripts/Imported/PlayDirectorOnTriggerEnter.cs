using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.InputSystem;

public class PlayDirectorOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private bool _repositionDirectorForEnemyAttack;
    [SerializeField] private bool _keepGunOnDuringCutscene;
    [SerializeField] private bool _SkippableCutscene;
    [SerializeField] private GameObject _directorPosition;
    [Header("True if you want to turn off raycasting")]
    [SerializeField] private bool _turnOffRaycast;
    [Header("If true, the cutscene is a ingame cutscene")]
    [SerializeField] private bool _InGameCutscene;
    [Header("This is the immediate event")]
    [SerializeField] private UnityEvent _event;
    [Header("This is a delayed event")]
    [SerializeField] private UnityEvent _delayedEvent;
    [Header("Event played at end of Timeline Playback")]
    [SerializeField] private UnityEvent _endTimelineEvent;
    [Header("Time delayed")]
    [SerializeField] private float _time;

    //private CharController _charController;
    private GameObject _inGameReticule;
    private GameObject _gunModel;



    private void Start()
    {
        //_charController = GameObject.Find("PlayerArmature").GetComponent<CharController>();
        _inGameReticule = GameObject.Find("Image_Reticule");
        _gunModel = GameObject.Find("USP_Pos");
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (_InGameCutscene == true)
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
        if (_SkippableCutscene == true)
        {
            // Get the total duration of the timeline
            double duration = _director.duration;

            // Calculate the time one second before the end
            double skipTime = duration - 1.0;

            // Ensure skipTime doesn't go below 0
            skipTime = Mathf.Max(0, (float)skipTime);

            // Set the Director's time to one second before the end
            _director.time = skipTime;

            // Play from the end
            _director.Play();
            _inGameReticule.SetActive(true);
            //_charController.InCutscene(false);
        }        
    }

    private void OnEnable()
    {
        _director.stopped += OnPlayableDirectorStopped;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_repositionDirectorForEnemyAttack == true)
            {
                _director.transform.position = _directorPosition.transform.position;
                _director.transform.rotation = _directorPosition.transform.rotation;
            }
            if (_inGameReticule != null)
            {
                if (_InGameCutscene == false)
                {
                    _inGameReticule.SetActive(false);
                }
            }
            _director.Play();
            _event.Invoke();
            HideGunDuringCutscene();
            if (_turnOffRaycast == true)
            {
                //_charController.InCutscene(true);
            }
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
            RevealGunAfterCutscene();
            _endTimelineEvent.Invoke();
            //_charController.InCutscene(false);
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
            if (_gunModel != null)
            {
                _gunModel.SetActive(false);
            }            
        }
        else
        {
            return;
        }
    }

    private void RevealGunAfterCutscene()
    {
        if (_gunModel != null)
        {
            _gunModel.SetActive(true);
        }            
    }
}
