using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Weapon _weapon;

    [SerializeField] private Transform[] _points;
    [SerializeField] private Player _player;
    [SerializeField] private Health _health;

    private void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        // StartCoroutine(Patroling());
    }

    private void Update()
    {
        if (CheckShootingPossibility())
        {
            _weapon.TryShoot(_player.transform.position - transform.position);
            LookToTarget(_player.transform.position);
        }
        else
        {
            FollowTarget(_player.transform);
            LookToTarget(_player.transform.position);
        }

    }

    private IEnumerator Patroling()
    {
        while (true)
        {
            Vector3 destination = GetRandomPoint().position;

            while (Vector3.Distance(transform.position, destination) > 0.1f)
            {
                _agent.SetDestination(destination);
                LookToTarget(destination);
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(0, 5));
        }
    }

    private Transform GetRandomPoint()
    {
        return _points[Random.Range(0, _points.Length)];
    }

    private void LookToTarget(Vector3 target)
    {
        Vector3 lookDirection = target - transform.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        _sprite.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
    }

    private void FollowTarget(Transform target)
    {
        _agent.SetDestination(target.position);
    }

    private bool CheckShootingPossibility()
    {
        Ray2D ray = new Ray2D(transform.position, _sprite.transform.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _weapon.Data.FireRange);

        // Debug.DrawRay(ray.origin, ray.direction * _weapon.Data.FireRange, Color.red);

        return (hit.collider != null && hit.collider.TryGetComponent(out Player player));
    }

    private void OnEnable()
    {
        _health.Overed += OnHealthOvered;
    }

    private void OnDisable()
    {
        _health.Overed += OnHealthOvered;
    }

    private void OnHealthOvered()
    {
        _weapon.Drop();
        Destroy(gameObject);
    }
}
