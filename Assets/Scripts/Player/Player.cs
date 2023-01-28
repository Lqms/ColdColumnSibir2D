using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;

    [SerializeField] private PlayerRotator _rotator;
    [SerializeField] private PhysicsMovement _physicsMovement;
    [SerializeField] private CombatSystem _combatSystem;
    [SerializeField] private PlayerCollisionHandler _collisionHandler;

    private void OnEnable()
    {
        _input.MoveKeyPressing += OnMoveKeyPressing;
        _input.ShootKeyPressing += OnShootKeyPressing;
        _input.ReloadKeyPressed += OnReloadKeyPressed;
        _input.ThrowGunKeyPressed += OnThrowGunKeyPressed;

        // ?
        _input.WeaponPickUpKeyPressed += OnWeaponPickUpKeyPressed;
    }

    private void OnDisable()
    {
        _input.MoveKeyPressing -= OnMoveKeyPressing;
        _input.ShootKeyPressing -= OnShootKeyPressing;
        _input.ReloadKeyPressed -= OnReloadKeyPressed;
        _input.ThrowGunKeyPressed -= OnThrowGunKeyPressed;

        // ?
        _input.WeaponPickUpKeyPressed -= OnWeaponPickUpKeyPressed;
    }

    private void OnMoveKeyPressing(Vector2 direction)
    {
        _physicsMovement.Move(direction);
    }

    private void OnShootKeyPressing()
    {
        _combatSystem.TryShoot(_rotator.LookDirection);
    }

    private void OnReloadKeyPressed()
    {
        _combatSystem.TryReload();
    }

    private void OnThrowGunKeyPressed()
    {
        _combatSystem.TryThrowWeapon();
    }

    // ?
    private void OnWeaponPickUpKeyPressed()
    {
        _combatSystem.TryPickUpWeapon();
    }
}
