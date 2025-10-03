using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting02 : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private float _maxDistance = 6f;
    [SerializeField] private LayerMask _interactableLayer;

    // Start is called before the first frame update
    void Start()
    {
        _cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        if(Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _interactableLayer))
        {
            Debug.Log("Looking at: " + hit.transform.name);

            if(Input.GetMouseButtonDown(0))
            {
                ItemInteraction item = hit.transform.GetComponent<ItemInteraction>();
                if(item != null)
                {
                    item.OnInteract();
                }
            }
        }
    }
}
