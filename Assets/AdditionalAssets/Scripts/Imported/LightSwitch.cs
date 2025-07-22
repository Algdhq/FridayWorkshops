using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LightSwitch : MonoBehaviour
{
    public enum FlickerType
    {
        OnFlickerOff,
        OffFlickerOn
    }

    [Header("Light Settings")]
    [SerializeField] private Light[] _lights;
    [SerializeField] private bool _offAtStart = false;
    [SerializeField] private bool _lightFlickering = false;
    [SerializeField] private FlickerType _flickerType;
    [SerializeField] private float _flickerSpeed = 1.0f;

    [Header("Animation Triggers")]
    [SerializeField] private Animator[] _animators;

    [Header("Optional Reflection Probes")]
    [SerializeField] private ReflectionProbe[] _reflectionProbes;

    [Header("Events")]
    [SerializeField] private UnityEvent _lightOn;
    [SerializeField] private UnityEvent _lightOff;

    private bool _lightState = true;
    private bool _keepLightsOff = false;
    private float _flickerMin;
    private float _flickerMax;

    private void Start()
    {
        SetFlickerRange();

        if (_offAtStart)
        {
            TurnLightOff();
            _lightState = false;
        }
        else
        {
            CheckLightStatus();
        }
    }

    public void TurnLightOn()
    {
        foreach (var light in _lights)
            light.enabled = true;

        if (_reflectionProbes != null)
        {
            foreach (var probe in _reflectionProbes)
                probe.enabled = true;
        }

        _lightOn?.Invoke();

        if (_lightFlickering)
            StartCoroutine(nameof(FlickerTimer));
    }

    public void TurnLightOff()
    {
        foreach (var light in _lights)
            light.enabled = false;

        if (_reflectionProbes != null)
        {
            foreach (var probe in _reflectionProbes)
                probe.enabled = false;
        }

        _lightOff?.Invoke();

        if (_lightFlickering)
            StopCoroutine(nameof(FlickerTimer));
    }

    public void CheckLightStatus()
    {
        if (_keepLightsOff)
            return;

        if (_lightState)
            TurnLightOff();
        else
            TurnLightOn();

        _lightState = !_lightState;
    }

    public void ManuallyAdjustLightBool(bool value) => _lightState = value;
    public void ManuallyAdjustKeepLightsOff(bool value) => _keepLightsOff = value;

    private void SetFlickerRange()
    {
        _flickerMin = 2f * _flickerSpeed;
        _flickerMax = 10f * _flickerSpeed;
    }

    private void PlayFlickerAnimation(string triggerName)
    {
        if (_animators == null) return;

        foreach (var anim in _animators)
            anim.SetTrigger(triggerName);
    }

    private IEnumerator FlickerTimer()
    {
        while (_lightFlickering)
        {
            yield return new WaitForSeconds(Random.Range(_flickerMin, _flickerMax));

            switch (_flickerType)
            {
                case FlickerType.OnFlickerOff:
                    PlayFlickerAnimation("Start");
                    break;
                case FlickerType.OffFlickerOn:
                    PlayFlickerAnimation("Flicker2");
                    break;
            }
        }
    }
}
