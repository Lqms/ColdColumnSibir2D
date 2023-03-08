using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // ����������� ����������� � ������� �����, ������� ���� ��������� ������ � ���������� ��������, ������� ������� ������ ������ � ��������� �����
    [SerializeField] private Transform _target;

    private const float BasePositionZ = -10;

    private void Update()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y, BasePositionZ);
    }
}
