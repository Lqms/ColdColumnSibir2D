using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class FieldOfHearing : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;

    private float _hearingRadius;
    private float _baseHearingRadius;

    public event UnityAction PlayerDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerCombat player))
        {
            player.Shooted += OnPlayerShooted;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerCombat player))
        {
            player.Shooted -= OnPlayerShooted;
        }
    }

    public void Init(float hearingRadius)
    {
        _hearingRadius = hearingRadius;
        _baseHearingRadius = hearingRadius;
        _collider.isTrigger = true;
        _collider.size = new Vector2(_baseHearingRadius, _baseHearingRadius);
    }

    public void SetNewRadius(float value)
    {
        _hearingRadius = value;
        _collider.size = new Vector2(value, value);
    }

    public void ResetRadius()
    {
        _hearingRadius = _baseHearingRadius;
        _collider.size = new Vector2(_baseHearingRadius, _baseHearingRadius);
    }

    private void OnPlayerShooted()
    {
        PlayerDetected?.Invoke();
    }
}
