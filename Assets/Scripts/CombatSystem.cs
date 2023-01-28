using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [SerializeField] private Transform _weaponPoint;

    private Weapon _currentWeapon;

    public void TryShoot(Vector2 direction)
    {
        if (_currentWeapon != null)
            _currentWeapon.TryShoot(direction);
    }

    public void TryReload()
    {
        if (_currentWeapon != null)
            _currentWeapon.Reload();
    }

    public void TryPickUpWeapon()
    {
        if (_currentWeapon != null)
            _currentWeapon.Drop();

        // ?
    }

    public void TryThrowWeapon()
    {
        if (_currentWeapon != null)
            _currentWeapon.Drop();
    }
}