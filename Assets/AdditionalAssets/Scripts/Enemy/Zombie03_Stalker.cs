using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AI;

public class Zombie03_Stalker : MonoBehaviour
{

    public enum Status
    {
        Undisturbed,
        Alerted,
        Stalking,
        Attack,
        Stunned
    }

    [SerializeField] private Status _status;

    private Animator _anim;
    [SerializeField] private UnityEngine.AI.NavMeshAgent _navMeshAgent;
    private GameObject _player;
    private bool _startNavMeshAgent;
    private bool _amAttacking;
    private bool _isStunned;
    private AudioSource _audioSource;

    [SerializeField] private List<GameObject> _hitBoxes = new List<GameObject>();
    [SerializeField] private LookAtConstraint _lookAtConstraint;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private float _stalkingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _player = GameObject.Find("PlayerArmature");
        RandomStart();
        _lookAtConstraint.constraintActive = false;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_isStunned)
        {
            _anim.SetFloat("Speed", 0f);
            return;
        }

        if (_startNavMeshAgent == true && !_amAttacking)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
            _anim.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
            if (_navMeshAgent.remainingDistance < 2.5f && !_navMeshAgent.pathPending)
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

            case Status.Attack:
                Attack();
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

        if (_status == Status.Undisturbed)
        {
            SetStatus(Status.Alerted);
        }
    }

    public void HitBody()
    {

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


    public void Attack()
    {
        float _attackIndex;
        float[] options = { 0f, 0.5f, 1f };
        _attackIndex = options[Random.Range(0, options.Length)];
        _anim.SetFloat("AttackFloat", _attackIndex);

        if (_amAttacking == false && !_isStunned)
        {
            _navMeshAgent.speed = 0;
            _anim.SetTrigger("Attack");
            _amAttacking = true;
            Invoke("ResumeChase", 1.5f);
        }
    }

    public void ResumeChase()
    {
        SetStatus(Status.Stalking);
        _startNavMeshAgent = true;
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = _stalkingSpeed;
        _amAttacking = false;
    }

    public void Stunned()
    {
        _sphereCollider.enabled = false;
        _isStunned = true;
        TurnOffColliders();
        _lookAtConstraint.constraintActive = false;
        _startNavMeshAgent = false;
        _navMeshAgent.speed = 0;
        _navMeshAgent.isStopped = true;
        AudioManager.Instance.PlayZombieDeathClip(Random.Range(0, 4));
        _anim.SetTrigger("Death");
        Invoke("Revive", Random.Range(10, 20));
    }

    public void Revive()
    {
        _isStunned = false;
        TurnOnColliders();
        _lookAtConstraint.constraintActive = true;
        AudioManager.Instance.PlayZombieJumpscareClip(Random.Range(0, 6));
        _anim.SetFloat("StandUpFloat", 0);
        _anim.SetTrigger("Revive");
        SetStatus(Status.Alerted);
        foreach(var h in _hitBoxes)
        {
            h.GetComponent<HitPoints>().ResetHealth();
        }
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

    public void FootStomp()
    {
        if (_amAttacking == false && !_isStunned)
        {
            if (_navMeshAgent.remainingDistance < 20f && !_navMeshAgent.pathPending)
            {
                PlayerManager.Instance.CamShake(PlayerManager.ShakeStrength.Weak);
                _audioSource.Play();
            }
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

        if (other.CompareTag("Player"))
        {
            SetStatus(Status.Alerted);
            _sphereCollider.radius = 7f;
        }
    }
}
