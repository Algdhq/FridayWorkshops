using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionLocation : MonoBehaviour
{
    private Transform _camera;
    private float _distance;
    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private Transform _newPosition;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_camera == null)
        {
            return;
        }

        Vector3 lookTarget = _camera.position;
        lookTarget.y = transform.position.y;
        transform.LookAt(lookTarget);

        _distance = Vector3.Distance(transform.position, _camera.position);
        float reduced = Mathf.Floor(_distance * 10f) / 10f;
        _distanceText.text = reduced.ToString("0.0" + "m");
    }

    public void SetNewMissionPosition(Transform value)
    {
        this.gameObject.SetActive(true);
        _newPosition = value;
        transform.position = _newPosition.position;
        AudioManager.Instance.PlayUISFXClip(1);
    }

    public void HideMissionPosition()
    {
        this.gameObject.SetActive(false);
    }
}
