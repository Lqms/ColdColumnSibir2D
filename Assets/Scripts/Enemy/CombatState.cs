using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatState : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _weaponPoint;

    private SpriteRenderer _spriteRenderer;
    private NavMeshAgent _agent;
    private Player _player;

    private void Update()
    {
        LookToTarget(_player.transform.position);

        if (CheckShootingPossibility())
        {
            _agent.SetDestination(transform.position);
            _weapon.TryShoot(_player.transform.position - transform.position);
        }
        else
        {
            _agent.SetDestination(_player.transform.position);
        }
    }

    public void Init(Player player, NavMeshAgent agent, SpriteRenderer spriteRenderer)
    {
        _agent = agent;
        _player = player;
        _spriteRenderer = spriteRenderer;
    }

    private bool CheckShootingPossibility()
    {
        Ray2D ray = new Ray2D(transform.position, _spriteRenderer.transform.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 12);

        /*
        Debug.DrawRay(ray.origin, ray.direction * 12, Color.red);
        var result = hit.collider != null && hit.collider.TryGetComponent(out Player player);
        print(result);
        */

        return hit.collider != null && hit.collider.TryGetComponent(out Player player); ;
    }

    private void LookToTarget(Vector3 target)
    {
        Vector3 lookDirection = target - transform.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        _spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
    }
}
