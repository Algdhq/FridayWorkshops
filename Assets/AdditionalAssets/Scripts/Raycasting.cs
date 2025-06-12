using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using StarterAssets;
using Cinemachine;
using UnityEngine;

public class Raycasting : MonoBehaviour
{
    public static Raycasting Instance { get; private set; }

    [SerializeField] private float rayDistance = 100f;
    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private StarterAssetsInputs _input;
    [SerializeField] private GameObject _normalCamera;
    private float aimingBlend = 0f;
    [SerializeField] private float aimingLerpTime = 0.25f;
    private bool _noRaycast;
    private bool _canInteract;
    private bool _aiming;
    private RaycastHit hit;
    private Animator _anim;
    private Transform playerRootTransform;
    private RotationConstraint _rotationConstraint;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _input = GameObject.Find("PlayerArmature").GetComponent<StarterAssetsInputs>();
        _anim = GameObject.Find("PlayerArmature").GetComponent<Animator>();
        playerRootTransform = GameObject.Find("PlayerArmature").transform;
        _rotationConstraint = GameObject.Find("CC_Base_Spine02").GetComponent<RotationConstraint>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);

        if (Physics.Raycast(ray, out hit, rayDistance, hitLayers))
        {
            _canInteract = true;
        }
        else
        {
            _canInteract = false;
        }
    }

    void Update()
    {
        if (_noRaycast == false)
        {
            _aiming = Input.GetMouseButton(1);

            _anim.SetBool("Aiming", _aiming);
            _normalCamera.SetActive(!_aiming);

            _rotationConstraint.enabled = _aiming;
            _rotationConstraint.weight = _aiming ? 1f : 0f;

            float targetBlend = _aiming ? 1f : 0f;
            aimingBlend = Mathf.MoveTowards(aimingBlend, targetBlend, Time.deltaTime / aimingLerpTime);
            _anim.SetFloat("AimingBool", aimingBlend);


            if (_aiming)
            {
                Vector3 lookDirection = _cam.transform.forward;
                lookDirection.y = 0; // Keep character upright
                if (lookDirection != Vector3.zero)
                {
                    playerRootTransform.forward = lookDirection;
                }
            }

            if (_canInteract == true && !_aiming && Input.GetMouseButtonDown(0))
            {
                Interact();
            }
        }        
    }

    public void NoRaycast(bool value)
    {
        _noRaycast = value;
    }

    private void Interact()
    {
        if (hit.collider.GetComponent<Iinteractable>() != null)
        {
            hit.collider.GetComponent<Iinteractable>().RunEvent();
        }
    }
}
