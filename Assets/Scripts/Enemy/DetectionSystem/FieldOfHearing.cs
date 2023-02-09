using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldOfHearing : MonoBehaviour
{
    public event UnityAction TargetDetected;

    public void StartReactToSound(PlayerCombat player)
    {
        player.Shooted += OnPlayerShooted;
    }

    public void StopReactToSound(PlayerCombat player)
    {
        player.Shooted -= OnPlayerShooted;
    }

    private void OnPlayerShooted(Weapon playerWeapon)
    {
        TargetDetected?.Invoke();
    }
}
