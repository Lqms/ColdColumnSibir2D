using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerRotator _rotator;
    [SerializeField] private Transform _weaponPoint;

    private Weapon _currentWeapon;

    private void OnEnable()
    {
        _input.ShootKeyPressing += OnShootKeyPressing;
        _input.ReloadKeyPressed += OnReloadKeyPressed;
        _input.ThrowGunKeyPressed += OnThrowGunKeyPressed;

        InteractableWeapon.PickedUp += OnWeaponPickedUp;
    }

    private void OnDisable()
    {
        _input.ShootKeyPressing -= OnShootKeyPressing;
        _input.ReloadKeyPressed -= OnReloadKeyPressed;

        InteractableWeapon.PickedUp -= OnWeaponPickedUp;
    }

    private void OnShootKeyPressing()
    {
        if (_currentWeapon != null)
            _currentWeapon.TryShoot(_rotator.LookDirection);
    }

    private void OnReloadKeyPressed()
    {
        if (_currentWeapon != null)
            _currentWeapon.Reload();
    }

    private void OnWeaponPickedUp(InteractableWeapon pickedUpWeapon)
    {
        if (_currentWeapon != null)
            _currentWeapon.Drop();

        _currentWeapon = pickedUpWeapon.Logic;
        _currentWeapon.OnPickUp(_weaponPoint);
    }

    private void OnThrowGunKeyPressed()
    {
        if (_currentWeapon != null)
            _currentWeapon.Drop();
    }
}