using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleLookAt : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera _mainCamera;
    void Start()
    {
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Awake()
    {
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void OnEnable()
    {
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_mainCamera.transform);
    }
}
