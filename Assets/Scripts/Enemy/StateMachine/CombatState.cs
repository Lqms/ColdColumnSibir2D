using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class CombatState : State
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private LayerMask _obstacleMask;

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
        float rangeOffset = 0.5f;
        Vector3 rayOffset = new Vector3(0.05f, 0, 0);

        /*
        var boxCastInfo = Physics2D.BoxCast(_weapon.transform.position, new Vector2(0.1f, 0.1f), 90, _weapon.transform.right, _weapon.Data.FireRange - rangeOffset, _obstacleMask);

        if (boxCastInfo.collider == null)
            return false;
        */

        RaycastHit2D raycastInfo = Physics2D.Raycast(_weapon.transform.position - rayOffset, _weapon.transform.right, _weapon.Data.FireRange - rangeOffset);

        return raycastInfo.collider != null && raycastInfo.collider.TryGetComponent(out Player player);
    }
}
