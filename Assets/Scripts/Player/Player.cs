using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerRotator _rotator;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerCombat _combat;
    [SerializeField] private PlayerCollisionHandler _collisionHandler;
    [SerializeField] private PlayerSoundManager _soundManager;
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        PlayerInput.MoveKeyPressing += OnMoveKeyPressing;
        PlayerInput.ShootKeyPressing += OnShootKeyPressing;
        PlayerInput.ThrowGunKeyPressed += OnThrowGunKeyPressed;
        PlayerInput.InteractKeyPressed += OnInteractKeyPressed;

        _combat.Shooted += OnShooted;
        _health.Overed += OnHealthOvered;
    }

    private void OnDisable()
    {
        PlayerInput.MoveKeyPressing -= OnMoveKeyPressing;
        PlayerInput.ShootKeyPressing -= OnShootKeyPressing;
        PlayerInput.ThrowGunKeyPressed -= OnThrowGunKeyPressed;
        PlayerInput.InteractKeyPressed -= OnInteractKeyPressed;

        _combat.Shooted -= OnShooted;
        _health.Overed -= OnHealthOvered;
    }

    private void OnShooted(Weapon weapon)
    {
        _soundManager.PlaySound(weapon.ShootSFX);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
