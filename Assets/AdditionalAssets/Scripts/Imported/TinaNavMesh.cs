using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TinaNavMesh : MonoBehaviour
{
    [Header("You CANNOT walk and kneel at the same time - one or the other")]
    public Transform _target;
    [SerializeField] private bool _walk;
    [SerializeField] private bool _kneel;
    private NavMeshAgent _agent;
    private Vector3 _destination;
    private Animator _anim;
    private float _velocity;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        SetDestination();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _velocity = _agent.velocity.magnitude / _agent.speed;

        if (_walk == false)
        {
            _anim.SetFloat("Blend", _velocity);
            _agent.speed = 3.5f;
        }

        if (_walk == true && _kneel == false)
        {
            _anim.SetFloat("Blend", _velocity / 2);
            _agent.speed = 1.0f;
        }

        if (_kneel == true)
        {
            _anim.SetBool("Kneeling", true);
            _agent.speed = 1.0f;
        }

        if (_kneel== false)
        {
            _anim.SetBool("Kneeling", false);            
        }

        SetDestination();
    }

    public void SetDestination()
    {    
        if (_target != null)
        {
            _destination = _target.position;
            _agent.destination = _destination;
        }        
    }

    public void StatusWalk(bool walk)
    {
        _walk = walk;        
    }

    public void StatusCrawl(bool kneel)
    {
        _kneel = kneel;
    }

    public void StatusDestination(Transform target)
    {
        _target = target;
    }
}
