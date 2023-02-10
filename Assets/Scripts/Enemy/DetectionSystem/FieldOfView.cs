using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class FieldOfView : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;

    private float _viewRadius = 10;
    private float _viewAngle = 180;
    private LayerMask _playerMask;
    private LayerMask _obstacleMask;
    private float _baseViewRadius;

    public event UnityAction PlayerDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerCombat player))
        {
            StartCoroutine(Viewing());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerCombat player))
        {
            StopCoroutine(Viewing());
        }
    }

    public void Init(float viewRadius, float viewAngle, LayerMask playerMask, LayerMask obstacleMask)
    {
        _viewRadius = viewRadius;
        _viewAngle = viewAngle;
        _playerMask = playerMask;
        _obstacleMask = obstacleMask;
        _baseViewRadius = _viewRadius;
        _collider.isTrigger = true;
        _collider.size = new Vector2(_baseViewRadius, _baseViewRadius);
    }

    public void SetNewRadius(float value)
    {
        _viewRadius = value;
        _collider.size = new Vector2(value, value);
    }

    public void ResetRadius()
    {
        _viewRadius = _baseViewRadius;
        _collider.size = new Vector2(_baseViewRadius, _baseViewRadius);
    }

    private IEnumerator Viewing()
    {
        while (true)
        {
            DetectPlayer();
            yield return null;
        }
    }

    private void DetectPlayer()
    {
        var targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, _viewRadius, _playerMask);

        foreach (var target in targetsInViewRadius)
        {
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.right, directionToTarget) < _viewAngle / 2)
            {
                float distantionToTarget = Vector2.Distance(transform.position, target.transform.position);

                if (Physics2D.Raycast(transform.position, directionToTarget, distantionToTarget, _obstacleMask) == false)
                {
                    PlayerDetected?.Invoke();
                }
            }
        }
    }
}
