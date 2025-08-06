using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CylinderLockPuzzle : MonoBehaviour
{
    [Header("Puzzle Info")]
    [SerializeField] private string _puzzleCode;
    [SerializeField] private UnityEvent _event;


    [Header("Objects In Puzzle")]
    [SerializeField] private GameObject _lockInteractive;
    [SerializeField] private GameObject _lockPuzzle;
    [SerializeField] private GameObject _VC;

    private bool _puzzleStarts;
    private float _rotationStep = 45f;
    [SerializeField] private int _currentCylinder = 0;

    private int _cylinder01Step = 0;
    [SerializeField] private GameObject _cylinder01;
    private int _cylinder02Step = 0;
    [SerializeField] private GameObject _cylinder02;
    private int _cylinder03Step = 0;
    [SerializeField] private GameObject _cylinder03;
    private string _cylinder01Letter = "";
    private string _cylinder02Letter = "";
    private string _cylinder03Letter = "";
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        EndPuzzle();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_puzzleStarts == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                EndPuzzle();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _currentCylinder = Mathf.Min(_currentCylinder + 1, 2);
                AudioManager.Instance.PlaySFXClip(0);
            }

            if(Input.GetKeyDown(KeyCode.A))
            {
                _currentCylinder = Mathf.Max(_currentCylinder - 1, 0);
                AudioManager.Instance.PlaySFXClip(0);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (_currentCylinder == 0)
                {
                    _cylinder01Step = (_cylinder01Step + 1) % 8;
                    _cylinder01.transform.localEulerAngles = new Vector3(_cylinder01Step * 45f, 0f, 0f);
                    Cylinder01Values();
                }
                if (_currentCylinder == 1)
                {
                    _cylinder02Step = (_cylinder02Step + 1) % 8;
                    _cylinder02.transform.localEulerAngles = new Vector3(_cylinder02Step * 45f, 0f, 0f);
                    Cylinder02Values();
                }
                if (_currentCylinder == 2)
                {
                    _cylinder03Step = (_cylinder03Step + 1) % 8;
                    _cylinder03.transform.localEulerAngles = new Vector3(_cylinder03Step * 45f, 0f, 0f);
                    Cylinder03Values();
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                if(_currentCylinder == 0)
                {
                    _cylinder01Step = (_cylinder01Step - 1 + 8) % 8;
                    _cylinder01.transform.localEulerAngles = new Vector3(_cylinder01Step * 45f, 0f, 0f);
                    Cylinder01Values();
                }
                if (_currentCylinder == 1)
                {
                    _cylinder02Step = (_cylinder02Step - 1 + 8) % 8;
                    _cylinder02.transform.localEulerAngles = new Vector3(_cylinder02Step * 45f, 0f, 0f);
                    Cylinder02Values();
                }
                if (_currentCylinder == 2)
                {
                    _cylinder03Step = (_cylinder03Step - 1 + 8) % 8;
                    _cylinder03.transform.localEulerAngles = new Vector3(_cylinder03Step * 45f, 0f, 0f);
                    Cylinder03Values();
                }
            }
        }        
    }

    public void CheckCode()
    {
        string enteredCode = _cylinder01Letter + _cylinder02Letter + _cylinder03Letter;

        if (enteredCode == _puzzleCode)
        {
            StartCoroutine("CompletedPuzzle");
            //Debug.Log("Puzzle is complete!  Code: " + enteredCode);
            AudioManager.Instance.PlaySFXClip(1);
        }

        else
        {
            //Debug.Log("Code incorrect.  Entered: " + enteredCode + " | Expected: " + _puzzleCode);
            AudioManager.Instance.PlaySFXClip(0);
        }
    }

    public void Cylinder01Values()
    {
        switch (_cylinder01Step)
        {
            case 0:
                _cylinder01Letter = "D";
                break;
            case 1:
                _cylinder01Letter = "C";
                break;
            case 2:
                _cylinder01Letter = "B";
                break;
            case 3:
                _cylinder01Letter = "A";
                break;
            case 4:
                _cylinder01Letter = "P";
                break;
            case 5:
                _cylinder01Letter = "O";
                break;
            case 6:
                _cylinder01Letter = "N";
                break;
            case 7:
                _cylinder01Letter = "E";
                break;
            default:
                _cylinder01Letter = "D";
                break;
        }
        //Debug.Log("Cylinder is on " + _cylinder01Step * 45 + " and Letter is " + _cylinder01Letter + " and case is " + _cylinder01Step);
        CheckCode();
    }

    public void Cylinder02Values()
    {
        switch (_cylinder02Step)
        {
            case 0:
                _cylinder02Letter = "N";
                break;
            case 1:
                _cylinder02Letter = "E";
                break;
            case 2:
                _cylinder02Letter = "D";
                break;
            case 3:
                _cylinder02Letter = "C";
                break;
            case 4:
                _cylinder02Letter = "B";
                break;
            case 5:
                _cylinder02Letter = "A";
                break;
            case 6:
                _cylinder02Letter = "P";
                break;
            case 7:
                _cylinder02Letter = "O";
                break;
            default:
                _cylinder02Letter = "N";
                break;
        }
        //Debug.Log("Cylinder is on " + _cylinder02Step * 45 + " and Letter is " + _cylinder02Letter + " and case is " + _cylinder02Step);
        CheckCode();
    }

    public void Cylinder03Values()
    {
        switch (_cylinder03Step)
        {
            case 0:
                _cylinder03Letter = "P";
                break;
            case 1:
                _cylinder03Letter = "O";
                break;
            case 2:
                _cylinder03Letter = "N";
                break;
            case 3:
                _cylinder03Letter = "E";
                break;
            case 4:
                _cylinder03Letter = "D";
                break;
            case 5:
                _cylinder03Letter = "C";
                break;
            case 6:
                _cylinder03Letter = "B";
                break;
            case 7:
                _cylinder03Letter = "A";
                break;
            default:
                _cylinder03Letter = "P";
                break;
        }
        //Debug.Log("Cylinder is on " + _cylinder03Step * 45 + " and Letter is " + _cylinder03Letter + " and case is " + _cylinder03Step);
        CheckCode();
    }

    public void StartPuzzle()
    {
        StartCoroutine("PuzzleStart");
    }

    public void EndPuzzle()
    {
        GameManager.Instance.UnPauseGame();
        _lockInteractive.SetActive(true);
        _lockPuzzle.SetActive(false);
        _VC.SetActive(false);
        _puzzleStarts = false;
    }

    IEnumerator CompletedPuzzle()
    {
        GameManager.Instance.UnPauseGame();
        _anim.SetTrigger("Open");
        yield return new WaitForSeconds(1.0f);
        _lockInteractive.SetActive(true);
        EndPuzzle();
        _event.Invoke();
        this.gameObject.SetActive(false);
    }

    IEnumerator PuzzleStart()
    {
        _VC.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.PauseGame();
        _lockInteractive.SetActive(false);
        _lockPuzzle.SetActive(true);
        _puzzleStarts = true;
    }
}
