using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using StarterAssets;
using Cinemachine;
using UnityEngine;
using EPOOutline;

public class Raycasting : MonoBehaviour
{
    public static Raycasting Instance { get; private set; }

    [SerializeField] private float rayDistance = 100f;
    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private StarterAssetsInputs _input;
    [SerializeField] private GameObject _normalCamera;
    [SerializeField] private GameObject _aimCamera;
    private float aimingBlend = 0f;
    [SerializeField] private float aimingLerpTime = 0.25f;
    private bool _noRaycast;
    private bool _canInteract;
    private bool _aiming;
    private GameObject _lastOutlinedObject;
    private RaycastHit hit;
    private Animator _anim;
    private Transform playerRootTransform;
    private RotationConstraint _rotationConstraint;
    private MeleeAttack _meleeAttack;
    [SerializeField] private float _meleeCooldown = 0.5f; // time between swings
    private bool _canMelee = true;

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

            // Enable outline on hit object
            if (hit.collider.TryGetComponent(out Outlinable outline))
            {
                outline.enabled = true;
            }

            // Disable all other outlines in the scene (except the one hit)
            foreach (Outlinable o in FindObjectsOfType<Outlinable>())
            {
                if (o.gameObject != hit.collider.gameObject)
                    o.enabled = false;
            }
        }
        else
        {
            _canInteract = false;

            // Disable all outlines if nothing is hit
            foreach (Outlinable o in FindObjectsOfType<Outlinable>())
            {
                o.enabled = false;
            }
        }
    }


    void Update()
    {
        if (_noRaycast == false)
        {
            _aiming = Input.GetMouseButton(1);

            _anim.SetBool("Aiming", _aiming);
            _normalCamera.SetActive(!_aiming);

            WeaponType weapon = InventoryManager.Instance._weaponType;

            if (_aiming && weapon != WeaponType.Melee)
            {
                _rotationConstraint.enabled = true;
                _rotationConstraint.weight = 1f;
            }
            else
            {
                _rotationConstraint.enabled = false;
                _rotationConstraint.weight = 0f;
            }


            float targetBlend = 0f;

            if (_aiming)
            {
                WeaponType currentWeaponType = InventoryManager.Instance.ReturnWeaponType();

                switch (currentWeaponType)
                {
                    case WeaponType.Melee:
                        targetBlend = 0.25f;
                        _aimCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = 50f;
                        break;
                    case WeaponType.Handgun:
                    case WeaponType.Magnum:
                    case WeaponType.Laser:
                        targetBlend = 0.5f;
                        _aimCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = 30f;
                        break;
                    case WeaponType.Grenade:
                    case WeaponType.Molotov:
                    case WeaponType.Mine:
                    case WeaponType.TNT:
                        // Optional: don't aim, or use a unique value later
                        targetBlend = 0.75f;
                        _aimCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = 50f;
                        break;
                    case WeaponType.Shotgun:
                    case WeaponType.SubMachineGun:
                    case WeaponType.MachineGun:
                    case WeaponType.Rifle:
                    case WeaponType.RPG:
                        targetBlend = 1.0f;
                        _aimCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = 30f;
                        break;
                    default:
                        targetBlend = 0f;
                        _aimCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = 50f;
                        break;
                }
            }
            aimingBlend = Mathf.MoveTowards(aimingBlend, targetBlend, Time.deltaTime / aimingLerpTime);
            _anim.SetFloat("AimingBool", aimingBlend);


            if (_aiming)
            {
                Vector3 lookDirection = _cam.transform.forward;
                lookDirection.y = 0; // Keep character upright
                if (lookDirection != Vector3.zero)
                {
                    playerRootTransform.forward = lookDirection;
                    WeaponType currentWeaponType = InventoryManager.Instance.ReturnWeaponType();
                    if (Input.GetMouseButtonDown(0) && currentWeaponType == WeaponType.Melee && _canMelee)
                    {
                        _anim.SetTrigger("Melee");
                        GameObject meleeWeapon = InventoryManager.Instance.GetCurrentWeapon();

                        if (meleeWeapon != null)
                        {
                            _meleeAttack = meleeWeapon.GetComponent<MeleeAttack>();
                            if (_meleeAttack != null)
                            {
                                _meleeAttack.Swing();
                                _canMelee = false;
                                StartCoroutine(MeleeCooldownRoutine());
                            }
                        }
                    }
                }
            }

            if (_canInteract == true && !_aiming && Input.GetMouseButtonDown(0))
            {
                Interact();
            }
        }        
    }

    private IEnumerator MeleeCooldownRoutine()
    {
        yield return new WaitForSeconds(_meleeCooldown);
        _canMelee = true;
    }

    public void NoRaycast(bool value)
    {
        _noRaycast = value;
    }

    public bool IsAiming()
    {
        return _aiming;
    }

    private void Interact()
    {
        if (hit.collider.GetComponent<Iinteractable>() != null)
        {
            hit.collider.GetComponent<Iinteractable>().RunEvent();
        }
    }
}
