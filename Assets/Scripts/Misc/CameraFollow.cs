using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // высчитываем направление в сторону мышки, смещаем туда положение камеры и активируем корутину, которая двигает камеру вместе с движением мышки
    [SerializeField] private Transform _target;

    private const float BasePositionZ = -10;

    private void Update()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y, BasePositionZ);
    }
}
