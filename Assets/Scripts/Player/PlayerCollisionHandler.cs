using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ???
public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;

    private Weapon _closestWeapon;

    public event UnityAction<Weapon> InteractedWithWeapon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Weapon weapon))
        {
            _closestWeapon = weapon;
            _input.WeaponPickUpKeyPressed += OnInteractKeyPressed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Weapon weapon))
        {
            _closestWeapon = null;
            _input.WeaponPickUpKeyPressed -= OnInteractKeyPressed;
        }
    }

    private void OnInteractKeyPressed()
    {
        InteractedWithWeapon?.Invoke(_closestWeapon);
    }
}
