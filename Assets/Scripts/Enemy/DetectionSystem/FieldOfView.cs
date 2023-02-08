using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float _viewRadius = 10;
    [SerializeField] private float _viewAngle = 180;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;

    public event UnityAction TargetDetected;

    private void FixedUpdate()
    {
        FindVisibleTargets();
    }

    private void FindVisibleTargets()
    {
        var targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, _viewRadius, _targetMask);

        foreach (var target in targetsInViewRadius)
        {
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.right, directionToTarget) < _viewAngle / 2)
            {
                float distantionToTarget = Vector2.Distance(transform.position, target.transform.position);

                if (Physics2D.Raycast(transform.position, directionToTarget, distantionToTarget, _obstacleMask) == false)
                {
                    TargetDetected?.Invoke();
                }
            }
        }
    }
}
