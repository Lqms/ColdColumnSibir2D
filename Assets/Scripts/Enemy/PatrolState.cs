using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class PatrolState : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform[] _points;
    [SerializeField] private SpriteRenderer _sprite;

    private void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        StartCoroutine(Patroling());
    }

    private IEnumerator Patroling()
    {
        while (true)
        {
            foreach (var point in _points)
            {
                Vector3 destination = point.position;
                LookToTarget(destination);
                _agent.SetDestination(destination);

                while (Vector3.Distance(transform.position, destination) > 0.1f)
                    yield return null;

                yield return new WaitForSeconds(Random.Range(0, 2));
            }
        }
    }

    private void LookToTarget(Vector3 target)
    {
        Vector3 lookDirection = target - transform.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        _sprite.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
    }
}
