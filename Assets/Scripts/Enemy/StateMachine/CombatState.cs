using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class CombatState : State
{
    [SerializeField] private Weapon _weapon;

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
        Vector3 rayOffset = new Vector3(0.05f, 0, 0);
        float rangeOffset = 0.5f;

        Ray2D ray = new Ray2D(_weapon.transform.position - rayOffset, _weapon.transform.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _weapon.Data.FireRange - rangeOffset);

        return hit.collider != null && hit.collider.TryGetComponent(out Player player);

        /*
        Ray2D ray = new Ray2D(_weapon.transform.position, _weapon.transform.right);
        var hits = Physics2D.BoxCastAll(ray.origin, new Vector2(0.1f, 0.1f), 90, ray.direction, _weapon.Data.FireRange - rangeOffset); 
         
        var results = "";

        foreach (var hit in hits)
            results += hit.collider.gameObject.name + " ";

        print(results);

        return hits.Length == 1 && hits[0].collider.TryGetComponent(out Player player);
        */
    }
}
