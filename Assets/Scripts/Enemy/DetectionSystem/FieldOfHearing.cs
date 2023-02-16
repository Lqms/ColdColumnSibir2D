using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class FieldOfHearing : MonoBehaviour
{
    private BoxCollider2D _collider;
    private Vector2 _baseAreaSize;

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

    public void Init(Vector2 areaSize)
    {
        _collider = GetComponent<BoxCollider2D>();
        _baseAreaSize = areaSize;
        _collider.size = areaSize;
        _collider.isTrigger = true;
    }

    public void SetNewAreaSize(Vector2 value)
    {
        _collider.size = value;
    }

    public void ResetAreaSize()
    {
        _collider.size = _baseAreaSize;
    }

    private void OnPlayerShooted()
    {
        PlayerDetected?.Invoke();
    }
}
