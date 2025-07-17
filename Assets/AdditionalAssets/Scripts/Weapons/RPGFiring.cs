using UnityEngine;

public class RPGFiring : MonoBehaviour
{
    [SerializeField] private GameObject _rocket;
    [SerializeField] private GameObject _rocketPosition;
    [SerializeField] private GameObject _rocketProp;

    private bool _hasReleasedClick = false;

    private void Update()
    {
        // Wait for the player to release left click before allowing fire again
        if (!Input.GetMouseButton(0))
        {
            _hasReleasedClick = true;
        }

        if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1) && _hasReleasedClick)
        {
            FireRocket();
            _hasReleasedClick = false; // prevent instant refire
        }
    }

    private void FireRocket()
    {
        Debug.Log("Fire Missile");
        Instantiate(_rocket, _rocketPosition.transform.position, _rocketPosition.transform.rotation);
        PlayerManager.Instance.CamShake(PlayerManager.ShakeStrength.Normal);
        AudioManager.Instance.PlayWeaponClip(1);
        _rocketProp.SetActive(false);
    }
}
