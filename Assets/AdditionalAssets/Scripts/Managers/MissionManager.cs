using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class MissionEvent
{
    public string name;
    public int missionID;
    public UnityEvent mission;
}

public class MissionManager : MonoBehaviour
{

    [SerializeField] private List<MissionEvent> _missions = new List<MissionEvent>();
    [SerializeField] private TextMeshProUGUI _missionText;
    private int _missionNumber;


    public void RunMission(int index)
    {
        if (index >= 0 && index < _missions.Count)
        {
            _missionNumber = index;
            if (_missions[index].mission != null)
            {
                _missions[index].mission.Invoke();
                Debug.Log("current mission is: " + _missions[index].name +
                    " | current ID is: " + +_missions[index].missionID);
            }
        }
    }

    public void UpdateMissionString(string value)
    {
        if (_missionText != null)
        {
            _missionText.text = "Mission: " + value;
            AudioManager.Instance.PlaySFXClip(4);
            Invoke("ClearText", 5.0f);
        }
    }

    private void OnValidate()
    {
        for (int i = 0; i < _missions.Count; i++)
        {
            _missions[i].missionID = i;
        }
    }

    private void ClearText()
    {
        if (_missionText != null)
        {
            _missionText.text = "";
        }
    }
}
