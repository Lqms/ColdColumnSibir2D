using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class CombatState : State
{
    [SerializeField] private Weapon _weapon;

    private float _rangeOffset = 0.5f;

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

    // либо переделать чтобы –ейкастќлл кидалс€ и если попалс€ обстакл, то фолс
    private bool CheckShootingPossibility()
    {
        Ray2D ray = new Ray2D(_weapon.ShootPoint.position, _weapon.ShootPoint.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _weapon.Data.FireRange - _rangeOffset);

        return hit.collider != null && hit.collider.TryGetComponent(out Player player);
    }
}
