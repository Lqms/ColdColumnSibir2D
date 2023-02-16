using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _main;

    private Vector3 _baseOffset;

    private void OnEnable()
    {
        PlayerInput.LookKeyPressed += OnLookKeyPressed;
    }

    private void OnDisable()
    {
        PlayerInput.LookKeyPressed -= OnLookKeyPressed;
    }

    private void Start()
    {
        _baseOffset = transform.position;
    }

    private void OnLookKeyPressed(bool pressed)
    {
        if (pressed)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _main.transform.position = mouseWorldPosition;
        }
        else
        {
            _main.transform.position = _baseOffset;
        }
    }
}
