using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode _moveUpKey = KeyCode.W;
    [SerializeField] private KeyCode _moveRightKey = KeyCode.D;
    [SerializeField] private KeyCode _moveDownKey = KeyCode.S;
    [SerializeField] private KeyCode _moveLeftKey = KeyCode.A;
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;
    [SerializeField] private KeyCode _interactKey = KeyCode.E;
    [SerializeField] private KeyCode _shootKey = KeyCode.Mouse0;

    public event UnityAction<Vector2> MoveKeyPressing;
    public event UnityAction ShootKeyPressing;
    public event UnityAction ReloadKeyPressed;
    public event UnityAction InteractKeyPressed;

    private void Update()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(_moveUpKey))
        {
            direction += Vector2.up;
        }

        if (Input.GetKey(_moveRightKey))
        {
            direction += Vector2.right;
        }

        if (Input.GetKey(_moveDownKey))
        {
            direction += Vector2.down;
        }

        if (Input.GetKey(_moveLeftKey))
        {
            direction += Vector2.left;
        }

        MoveKeyPressing?.Invoke(direction);

        if (Input.GetKey(_shootKey))
        {
            ShootKeyPressing?.Invoke();
        }

        if (Input.GetKeyDown(_reloadKey))
        {
            ReloadKeyPressed?.Invoke();
        }

        if (Input.GetKeyDown(_interactKey))
        {
            InteractKeyPressed?.Invoke();
        }
    }
}
