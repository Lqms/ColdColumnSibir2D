using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed = 250;

    private void OnEnable()
    {
        _input.MoveKeyPressing += OnMoveKeyPressing;
    }

    private void OnDisable()
    {
        _input.MoveKeyPressing -= OnMoveKeyPressing;
    }

    private void OnMoveKeyPressing(Vector2 direction)
    {
        _rigidbody.velocity = direction * _speed;
    }
}
