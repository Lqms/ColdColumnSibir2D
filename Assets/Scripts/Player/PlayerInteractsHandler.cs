using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractsHandler : MonoBehaviour
{
    [SerializeField] private PlayerCollisionHandler _collisionHandler;
    [SerializeField] private PlayerInput _input;

    private void OnEnable()
    {
        _input.InteractKeyPressed += OnInteractKeyPressed;
    }

    private void OnDisable()
    {
        _input.InteractKeyPressed -= OnInteractKeyPressed;
    }

    private void OnInteractKeyPressed()
    {
        var closesObject = _collisionHandler.DetermineClosestObject();

        if (closesObject != null)
            closesObject.Interact();
    }
}
