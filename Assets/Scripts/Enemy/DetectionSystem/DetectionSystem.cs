using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FieldOfHearing))]
[RequireComponent(typeof(FieldOfView))]
[RequireComponent(typeof(Collider2D))]
public class DetectionSystem : MonoBehaviour
{
    [SerializeField] private FieldOfHearing _fieldOfHearing;
    [SerializeField] private FieldOfView _fieldOfView;

    public event UnityAction PlayerDetected;

    private void OnEnable()
    {
        _fieldOfHearing.TargetDetected += OnTargetDetected;
        _fieldOfView.TargetDetected += OnTargetDetected;
    }

    private void OnDisable()
    {
        _fieldOfHearing.TargetDetected -= OnTargetDetected;
        _fieldOfView.TargetDetected -= OnTargetDetected;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            SwitchHearingState(true);
            SwitchVisionState(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            SwitchHearingState(false);
            SwitchVisionState(false);
        }
    }

    private void OnTargetDetected()
    {
        PlayerDetected?.Invoke();
    }

    public void SwitchHearingState(bool newState)
    {
        _fieldOfHearing.enabled = newState;
    }

    public void SwitchVisionState(bool newState)
    {
        _fieldOfView.enabled = newState;
    }
}
