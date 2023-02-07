using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatState : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Transform _weaponPoint;

    private void Update()
    {
        LookToTarget(_player.transform.position);

        if (CheckShootingPossibility())
        {
            _agent.enabled = false;
            _weapon.TryShoot(_player.transform.position - transform.position);
        }
        else
        {
            _agent.enabled = true;
            _agent.SetDestination(_player.transform.position);
        }
    }

    private bool CheckShootingPossibility()
    {
        Ray2D ray = new Ray2D(transform.position, _sprite.transform.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 12);

        Debug.DrawRay(ray.origin, ray.direction * 12, Color.red);
        var result = hit.collider != null && hit.collider.TryGetComponent(out Player player);
        print(result);

        return result;
    }

    private void LookToTarget(Vector3 target)
    {
        Vector3 lookDirection = target - transform.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        _sprite.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
    }
}
