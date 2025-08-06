using UnityEngine;
using UnityEngine.Animations;
using System.Collections;

public class AimConstraintFix : MonoBehaviour
{
    [SerializeField] private AimConstraint _aimConstraint;
    [SerializeField] private float _lerpSpeed = 5f;

    private bool _wasAiming;
    private Coroutine _lerpCoroutine;

    private void OnEnable()
    {
        if (_aimConstraint != null)
        {
            _aimConstraint.weight = 0f;
            _aimConstraint.constraintActive = false;
        }

        _wasAiming = false;

        if (_lerpCoroutine != null)
            StopCoroutine(_lerpCoroutine);

        StartCoroutine(MonitorAiming());
    }

    private IEnumerator MonitorAiming()
    {
        while (true)
        {
            bool isAiming = Raycasting.Instance != null && Raycasting.Instance.IsAiming();

            if (isAiming != _wasAiming)
            {
                _wasAiming = isAiming;
                float target = isAiming ? 1f : 0f;

                if (_lerpCoroutine != null)
                    StopCoroutine(_lerpCoroutine);

                if (isAiming && !_aimConstraint.constraintActive)
                    _aimConstraint.constraintActive = true;

                _lerpCoroutine = StartCoroutine(LerpWeight(target, isAiming));
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator LerpWeight(float target, bool isAiming)
    {
        float start = _aimConstraint.weight;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * _lerpSpeed;
            _aimConstraint.weight = Mathf.Lerp(start, target, t);
            yield return null;
        }

        _aimConstraint.weight = target;

        // Disable constraint if we're ending the aim
        if (!isAiming)
            _aimConstraint.constraintActive = false;
    }
}
