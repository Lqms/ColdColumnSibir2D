using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgressiveBehaviour : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Transform _weaponPoint;

    private void Start()
    {
        _weapon.OnPickUp(_weaponPoint);
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

    private bool CheckShootingPossibility()
    {
        Ray2D ray = new Ray2D(transform.position, _sprite.transform.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _weapon.Data.FireRange);

        // Debug.DrawRay(ray.origin, ray.direction * _weapon.Data.FireRange, Color.red);

        return (hit.collider != null && hit.collider.TryGetComponent(out Player player));
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
}
