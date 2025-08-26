using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;
using UnityEngine.AI;

public class Zombie05_SimpleStalkerNoStun : MonoBehaviour
{
    //start undistrubed - laying on floor randomly
    //alerted - rising up from the ground with approiate standing animation
    //stalking - walking toward player chasing them down
    //attack - do attack animation - stop walking, then after animation is over, resume if he is no longer close
    //death - do deth animaton, turn off all colliders, then stop moving
    public enum Status
    {
        Undisturbed,
        Alerted,
        Stalking,
        Attack,
        Death
    }

    [SerializeField] private Status _status;

    private Animator _anim;
    private GameObject _player;
    private bool _startNavMeshAgent;

    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private LookAtConstraint _lookAtConstraint;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private float _stalkingSpeed;
    [SerializeField] private int _exp;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _player = GameObject.Find("PlayerArmature");
        RandomStart();
        _lookAtConstraint.constraintActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_startNavMeshAgent == true)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
            _anim.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
            if (_navMeshAgent.remainingDistance < 2.0f && !_navMeshAgent.pathPending)
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

        switch(_status)
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
            case Status.Death:
                Death();
                break;
            default:
                Debug.Log("Unhandled zombie stats" + _status);
                break;
        }
    }

    public void HitEnemy()
    {
        if(_status == Status.Undisturbed)
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
        Invoke("ResumeChase", 3.0f);
    }

    public void Stalking()
    {
        _startNavMeshAgent = true;
        _sphereCollider.enabled = false;
    }

    public void Attack()
    {

    }

    public void ResumeChase()
    {
        _navMeshAgent.speed = _stalkingSpeed;
        SetStatus(Status.Stalking);
    }

    public void Death()
    {
        Debug.Log("The enemy is dead");
    }

    public void RandomStart()
    {
        float _startIndex;
        float[] options = { 0f, 0.2f, 0.4f, 0.6f, 0.8f, 1.0f };
        _startIndex = options[Random.Range(0, options.Length)];
        _anim.SetFloat("StartingFloatPositionOnFloor", _startIndex);

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
        }
    }
}
