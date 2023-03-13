using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerRotator _rotator;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerCombat _combat;
    [SerializeField] private PlayerCollisionHandler _collisionHandler;
    [SerializeField] private Health _health;

    public event UnityAction Died;

    private void OnEnable()
    {
        PlayerInput.MoveKeyPressing += OnMoveKeyPressing;
        PlayerInput.ShootKeyPressing += OnShootKeyPressing;
        PlayerInput.ThrowGunKeyPressed += OnThrowGunKeyPressed;
        PlayerInput.InteractKeyPressed += OnInteractKeyPressed;

        PlayerInput.LookKeyPressed += OnLookKeyPressed;
        _health.Overed += OnHealthOvered;
    }

    private void OnDisable()
    {
        PlayerInput.MoveKeyPressing -= OnMoveKeyPressing;
        PlayerInput.ShootKeyPressing -= OnShootKeyPressing;
        PlayerInput.ThrowGunKeyPressed -= OnThrowGunKeyPressed;
        PlayerInput.InteractKeyPressed -= OnInteractKeyPressed;

        PlayerInput.LookKeyPressed -= OnLookKeyPressed;
        _health.Overed -= OnHealthOvered;
    }

    private void OnLookKeyPressed(bool isKeyDown)
    {

    }

    private void OnMoveKeyPressing(Vector2 direction)
    {
        _movement.Move(direction);
    }

    private void OnShootKeyPressing()
    {
        _combat.TryShoot(_rotator.LookDirection);
    }

    private void OnThrowGunKeyPressed()
    {
        _combat.TryThrowWeapon();
    }

    private void OnInteractKeyPressed()
    {
        if (_collisionHandler.ClosestWeapon != null)
            _combat.PickUpWeapon(_collisionHandler.ClosestWeapon);
    }

    private void OnHealthOvered()
    {
        Died?.Invoke();
        gameObject.SetActive(false);
    }
}
