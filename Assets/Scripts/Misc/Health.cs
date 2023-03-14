using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private const float Max = 100; // test 100
    private float _current;

    public event UnityAction Overed;

    private void Start()
    {
        _current = Max;
    }

    public void ApplyDamage(float amount)
    {
        _current = Mathf.Clamp(_current - amount, 0, _current);
        print(_current);

        if (_current == 0)
        {
            Overed?.Invoke();
        }
    }
}
