using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerRotator _rotator;
    [SerializeField] private Weapon _currentWeapon;

    private void OnEnable()
    {
        _input.ShootKeyPressing += OnShootKeyPressing;
        _input.ReloadKeyPressed += OnReloadKeyPressed;

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
        Transform currentWeaponParent = _currentWeapon.transform.parent;
        _currentWeapon.transform.rotation = Quaternion.Euler(Vector3.zero);
        _currentWeapon.Collider.isTrigger = true;
        _currentWeapon.transform.parent = null;

        _currentWeapon = pickedUpWeapon.Logic;

        _currentWeapon.Collider.isTrigger = false;
        _currentWeapon.transform.parent = currentWeaponParent;
        _currentWeapon.transform.position = currentWeaponParent.position;
        _currentWeapon.transform.rotation = currentWeaponParent.rotation;
    }
}