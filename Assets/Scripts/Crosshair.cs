using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private const float WorldPositionZ = 10;

    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, WorldPositionZ));
    }
}
