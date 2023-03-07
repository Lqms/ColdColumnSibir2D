using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int _enemiesLeft;

    private void Start()
    {
        _enemiesLeft = FindObjectsOfType<Enemy>().Length;
    }

    private void OnEnable()
    {
        Enemy.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        Enemy.Died -= OnEnemyDied;
    }

    private void OnEnemyDied()
    {
        _enemiesLeft--;

        if (_enemiesLeft == 0)
        {
            print("win"); 
            // ����� ��������� �������, �� ������� ��������� LevelInfoDisplay ��������� ����� � ���,
            // ��� �� �������, ���������� ������� ��� ���������, �� ��� �� ���� ������� ������ ������
            // ��� ������ �������������� ����� ��������� � ���� ����� ��� �����, �� ��������� � ���������� �������, ��� ����� � ������� ������� ����� � ������� "������"
            // ����� ��� ����� ����� ������ � �.�.
        }
    }
}
