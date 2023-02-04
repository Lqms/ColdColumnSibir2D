using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private Weapon _currentWeapon;

    public event UnityAction<int> BulletsChanged;

    private void Start()
    {
        if (_currentWeapon != null)
            BulletsChanged?.Invoke(_currentWeapon.BulletsCount);
    }

    public void TryShoot(Vector2 direction)
    {
        if (_currentWeapon == null)
            return;

        if (_currentWeapon.TryShoot(direction))
            BulletsChanged?.Invoke(_currentWeapon.BulletsCount);
    }

    public void TryThrowWeapon()
    {
        if (_currentWeapon == null)
            return;

        _currentWeapon.Drop();
        _currentWeapon = null;

        BulletsChanged?.Invoke(0);
    }

    public void PickUpWeapon(Weapon weapon)
    {
        if (_currentWeapon != null)
            _currentWeapon.Drop();

        _currentWeapon = weapon;
        _currentWeapon.OnPickUp(_weaponPoint);

        BulletsChanged?.Invoke(_currentWeapon.BulletsCount);
    }
}