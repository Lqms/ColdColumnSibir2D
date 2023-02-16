using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _main;
    [SerializeField] private Crosshair _crosshair;

    private Transform _baseParent;
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
        _baseParent = transform.parent;
        _baseOffset = transform.localPosition;
    }

    // Переделать за счет виртуальной камеры (Cinemachine)
    private void OnLookKeyPressed(bool pressed)
    {
        if (pressed)
        {
            _main.transform.parent = _crosshair.transform;
            _main.transform.position = _crosshair.transform.position;
        }
        else
        {
            _main.transform.parent = _baseParent;
            _main.transform.localPosition = _baseOffset;
        }
    }
}
