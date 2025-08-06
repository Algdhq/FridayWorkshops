using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AI;



public class Zombie : MonoBehaviour
{
    public enum Status
    {
        Undisturbed,
        Alerted,
        Chase,
        Attack,
        Death,
        Stunned
    }

    [SerializeField] private Status _status;

    private Animator _anim;
    private bool _playerInRange;
    private int _wasShot;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    private GameObject _player;
    private bool _startNavMeshAgent;
    private bool _isStunned;
    private bool _amAttacking;
    private bool _isDead;

    [SerializeField] private List<GameObject> _hitBoxes = new List<GameObject>();
    [SerializeField] private LookAtConstraint _lookAtConstraint;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private GameObject _bloodDecal;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _player = GameObject.Find("PlayerArmature");
        RandomStart();
        _lookAtConstraint.constraintActive = false;
    }
    private void Update()
    {
        if (_startNavMeshAgent == true)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
            _anim.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
            if(_navMeshAgent.remainingDistance < 1.5f && !_navMeshAgent.pathPending)
            {
                SetStatus(Status.Attack);
            }
        }
    }

    public void SetStatus(Status newStatus)
    {
        if (_status != newStatus)
        {
            _status = newStatus;
        }

        switch (_status)
        {
            case Status.Undisturbed:
                Undisturbed();
                break;

            case Status.Alerted:
                Alerted();
                break;

            case Status.Attack:
                Attack();
                break;

            case Status.Chase:
                Chase();
                break;

            case Status.Death:
                Death();
                break;

            case Status.Stunned:
                Stunned();
                break;

            default:
                Debug.LogWarning("Unhandled zombie status: " + _status);
                break;
        }
    }

    public void Headshot()
    {
        _wasShot++;

        if (_status == Status.Chase && !_isStunned)
        {
            RollStunned();
            if (_isStunned)
            {
                SetStatus(Status.Stunned);
            }
        }
        if (_status == Status.Alerted)
        {
            SetStatus(Status.Chase);
        }
        if (_status == Status.Undisturbed)
        {
            SetStatus(Status.Alerted);
        }
    }

    public void HitBody()
    {
        _wasShot++;

        if (_status == Status.Chase && !_isStunned)
        {
            RollStunned();
            if (_isStunned)
            {
                SetStatus(Status.Stunned);
            }
        }
        if (_status == Status.Alerted)
        {
            SetStatus(Status.Chase);
        }
        if (_status == Status.Undisturbed)
        {
            SetStatus(Status.Alerted);
        }
    }

    public void Undisturbed()
    {
        _lookAtConstraint.constraintActive = false;
    }

    public void Alerted()
    {
        _anim.SetTrigger("Alert");
        PlayerManager.Instance.CamShake(PlayerManager.ShakeStrength.Normal);
        AudioManager.Instance.PlayZombieJumpscareClip(Random.Range(0, 6));
        AudioManager.Instance.PlayLongStingerClip(0);
        _lookAtConstraint.constraintActive = true;
        if (_wasShot == 1)
        {
            _playerInRange = true;
            StartCoroutine("ForceEngagement");
        }
        if (_wasShot > 1)
        {
            SetStatus(Status.Chase);
        }
    }

    public void Chase()
    {
        _startNavMeshAgent = true;
        _navMeshAgent.speed = 6f;
        _lookAtConstraint.constraintActive = false;
        _sphereCollider.enabled = false;
        AudioManager.Instance.PlayLongStingerClip(1);
        AudioManager.Instance.PlayZombieJumpscareClip(Random.Range(0, 6));
        StopCoroutine("ForceEngagement");
        StopCoroutine("EngagementCountdown");
    }

    public void RollStunned()
    {
        int roll = Random.Range(0, 100);
        if (roll < 50)
        {
            _isStunned = true;
        }
        else
        {
            _isStunned = false;
        }
    }

    public void Stunned()
    {
        _navMeshAgent.speed = 0;
        _anim.SetBool("FallDown", true);
        AudioManager.Instance.PlayZombieDeathClip(Random.Range(0, 4));
        StopAllCoroutines();
        Invoke("GetUpFromStunned", Random.Range(2.0f, 3.0f));
    }

    public void GetUpFromStunned()
    {
        _anim.SetBool("FallDown", false);
        _isStunned = false;
        SetStatus(Status.Chase);
    }

    public void Attack()
    {
        if(_amAttacking == false && !_isDead)
        {
            _navMeshAgent.speed = 0;
            _anim.SetTrigger("Attack");
            StopAllCoroutines();
            _amAttacking = true;            
            Invoke("ResumeChase", 2f);
        }        
    }

    public void ResumeChase()
    {
        SetStatus(Status.Chase);
        _amAttacking = false;
    }

    public void Death()
    {
        CancelInvoke("ResumeChase");
        CancelInvoke("GetUpFromStunned");
        StopAllCoroutines();
        _sphereCollider.enabled = false;
        AudioManager.Instance.PlayZombieDeathClip(Random.Range(0, 4));
        AudioManager.Instance.PlayStingerClip(1);
        AudioManager.Instance.PlayLongStingerClip(-1);
        _isDead = true;
        _bloodDecal.SetActive(true);
        TurnOffColliders();
        _startNavMeshAgent = false;
        _navMeshAgent.speed = 0;
        _navMeshAgent.isStopped = true;
        StopAllCoroutines();
        _anim.SetTrigger("Death");
    }

    public void TurnOffColliders()
    {
        foreach(var h in _hitBoxes)
        {
            h.SetActive(false);
        }
    }

    public void RandomStart()
    {
        float _startIndex;
        float[] options = { 0f, 0.2f, 0.4f, 0.6f, 0.8f, 1f };
        _startIndex = options[Random.Range(0, options.Length)];
        _anim.SetFloat("StartFloatPositionOnFloor", _startIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetStatus(Status.Alerted);
            _sphereCollider.radius = 7f;
            _playerInRange = true;
            StartCoroutine("EngagementCountdown");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_status == Status.Alerted)
            {
                SetStatus(Status.Undisturbed);
                _sphereCollider.radius = 5f;
                _playerInRange = false;
                StopCoroutine("EngagementCountdown");
            }            
        }
    }

    IEnumerator EngagementCountdown()
    {
        yield return new WaitForSeconds(5f);
        if (_playerInRange == true)
        {
            SetStatus(Status.Chase);
        }
    }

    IEnumerator ForceEngagement()
    {
        yield return new WaitForSeconds(5f);
        SetStatus(Status.Chase);
    }
}
