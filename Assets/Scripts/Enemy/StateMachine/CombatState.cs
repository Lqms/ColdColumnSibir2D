using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatState : State
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _weaponPoint;

    private void Update()
    {
        TurnToTarget(Player.transform.position);

        if (CheckShootingPossibility())
        {
            Agent.SetDestination(transform.position);
            _weapon.TryShoot(Player.transform.position - transform.position);
        }
        else
        {
            Agent.SetDestination(Player.transform.position);
        }
    }

    private bool CheckShootingPossibility()
    {
        Ray2D ray = new Ray2D(transform.position, SpriteRenderer.transform.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 12);

        /*
        Debug.DrawRay(ray.origin, ray.direction * 12, Color.red);
        var result = hit.collider != null && hit.collider.TryGetComponent(out Player player);
        print(result);
        */

        return hit.collider != null && hit.collider.TryGetComponent(out Player player); ;
    }
}
