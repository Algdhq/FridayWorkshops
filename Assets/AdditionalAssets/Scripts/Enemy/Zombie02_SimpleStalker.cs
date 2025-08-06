using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AI;

public class Zombie02_SimpleStalker : MonoBehaviour
{

    public enum Status
    {
        Undisturbed,
        Alerted,
        Stalking,
        Stunned,
        Attack,
        Death
    }

    [SerializeField] private Status _status;

    private Animator _anim;
    private int _wasShot;
    [SerializeField] private UnityEngine.AI.NavMeshAgent _navMeshAgent;
    private GameObject _player;
    private bool _startNavMeshAgent;
    private bool _isStunned;
    private bool _amAttacking;
    private bool _isDead;

    [SerializeField] private List<GameObject> _hitBoxes = new List<GameObject>();
    [SerializeField] private LookAtConstraint _lookAtConstraint;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private GameObject _bloodDecal;
    [SerializeField] private float _stalkingSpeed;

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
        if (_isStunned || _isDead)
        {
            _anim.SetFloat("Speed", 0f);
            return;
        }

        if (_startNavMeshAgent == true)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
            _anim.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
            if (_navMeshAgent.remainingDistance < 1.5f && !_navMeshAgent.pathPending)
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

            case Status.Stalking:
                Stalking();
                break;

            case Status.Stunned:
                Stunned();
                break;

            case Status.Attack:
                Attack();
                break;

            case Status.Death:
                Death();
                break;            

            default:
                Debug.LogWarning("Unhandled zombie status: " + _status);
                break;
        }
    }

    public void Headshot()
    {
        _wasShot++;

        if (_status == Status.Stalking && !_isStunned)
        {
            RollStunned();
            if (_isStunned)
            {
                SetStatus(Status.Stunned);
            }
        }
 
        if (_status == Status.Undisturbed)
        {
            SetStatus(Status.Alerted);
        }
    }

    public void HitBody()
    {
        _wasShot++;

        if (_status == Status.Stalking && !_isStunned)
        {
            RollStunned();
            if (_isStunned)
            {
                SetStatus(Status.Stunned);
            }
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
        _lookAtConstraint.constraintActive = true;
        AudioManager.Instance.PlayZombieJumpscareClip(Random.Range(7, 10));
        Invoke("ResumeChase", 3.0f);
    }

    public void Stalking()
    {
        _startNavMeshAgent = true;
        _navMeshAgent.speed = _stalkingSpeed;
        _sphereCollider.enabled = false;
    }

    public void RollStunned()
    {
        int roll = Random.Range(0, 100);
        if (roll < 20)
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
        AudioManager.Instance.PlayEnemyClip(-1);
        _lookAtConstraint.constraintActive = false;
        Invoke("GetUpFromStunned", Random.Range(5.0f, 10.0f));
        AudioManager.Instance.PlayZombieDeathClip(Random.Range(0, 4));
    }

    public void GetUpFromStunned()
    {
        _anim.SetBool("FallDown", false);
        _isStunned = false;
        _lookAtConstraint.constraintActive = true;
        SetStatus(Status.Stalking);
        int RR = Random.Range(0, 10);
        if (RR >= 8)
        {
            AudioManager.Instance.PlayZombieJumpscareClip(Random.Range(0, 6));
            PlayerManager.Instance.CamShake(PlayerManager.ShakeStrength.Normal);
        }        
        else if (RR < 8)
        {
            AudioManager.Instance.PlayZombieJumpscareClip(Random.Range(7, 10));
        }
    }

    public void Attack()
    {
        if (_amAttacking == false && !_isDead)
        {
            _navMeshAgent.speed = 0;
            _anim.SetTrigger("Attack");
            _amAttacking = true;
            Invoke("ResumeChase", 2f);
        }
    }

    public void ResumeChase()
    {
        SetStatus(Status.Stalking);
        _amAttacking = false;
    }

    public void Death()
    {
        _sphereCollider.enabled = false;
        _isDead = true;
        _bloodDecal.SetActive(true);
        TurnOffColliders();
        _lookAtConstraint.constraintActive = false;
        _startNavMeshAgent = false;
        _navMeshAgent.speed = 0;
        _navMeshAgent.isStopped = true;
        AudioManager.Instance.PlayZombieDeathClip(Random.Range(0, 4));
        _anim.SetTrigger("Death");
    }

    public void TurnOffColliders()
    {
        foreach (var h in _hitBoxes)
        {
            h.SetActive(false);
        }
    }

    public void TurnOnColliders()
    {
        foreach (var h in _hitBoxes)
        {
            h.SetActive(true);
        }
    }

    public void RandomStart()
    {
        float _startIndex;
        float[] options = { 0f, 0.2f, 0.4f, 0.6f, 0.8f, 1f };
        _startIndex = options[Random.Range(0, options.Length)];        
        _anim.SetFloat("StartFloatPositionOnFloor", _startIndex);

        if (_startIndex == 0f || _startIndex == 0.2f)
        {
            _anim.SetFloat("StandUpFloat", 0);
        }
        else
        {
            _anim.SetFloat("StandUpFloat", 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isStunned || _isDead)
            return;

        if (other.CompareTag("Player"))
        {
            SetStatus(Status.Alerted);
            _sphereCollider.radius = 7f;
        }
    }
}
