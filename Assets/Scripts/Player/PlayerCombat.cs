using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerRotator _rotator;
    [SerializeField] private Weapon _weapon;

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
        if (_weapon != null)
            _weapon.TryShoot(_rotator.LookDirection);
    }

    private void OnReloadKeyPressed()
    {
        if (_weapon != null)
            _weapon.Reload();
    }

    private void OnWeaponPickedUp(Weapon weapon)
    {
        Transform currentParent = _weapon.transform.parent;
        _weapon.transform.parent = null;
        _weapon = weapon;
        _weapon.transform.parent = currentParent;
    }
}
