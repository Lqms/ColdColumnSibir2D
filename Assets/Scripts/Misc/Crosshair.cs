using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Crosshair : MonoBehaviour, IPointerMoveHandler
{
    private Vector3 _currentMousePosition;

    public void OnPointerMove(PointerEventData eventData)
    {
        print(1);
    }

    private void Start()
    {
        _currentMousePosition = Input.mousePosition;
    }

    private void Update()
    {
        if (_currentMousePosition != Input.mousePosition)
        {
            print("mouse is moving");
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            _currentMousePosition = Input.mousePosition;
        }
        else
        {
            print("mouse is stopped");
        }
    }
}
