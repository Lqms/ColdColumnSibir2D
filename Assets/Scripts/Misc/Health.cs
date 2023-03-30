using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private Transform _head;
    [SerializeField] private float _headRadius = 1;

    private const float Max = 1_000;
    private float _current;

    public event UnityAction Overed;
    public event UnityAction HeadShot;

    private void Start()
    {
        _current = Max;
    }

    public void ApplyDamage(float amount, Vector3 bulletPosition)
    {
        _current = Mathf.Clamp(_current - amount, 0, _current);
        print(_current);

        if (Vector3.Distance(bulletPosition, _head.position) < _headRadius)
        {
            print("headshot");
            HeadShot?.Invoke();
        }

        if (_current == 0)
        {
            Overed?.Invoke();
        }
    }
}
