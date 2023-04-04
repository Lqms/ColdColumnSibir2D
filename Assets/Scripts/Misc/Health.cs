using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private bool _isHead = false;

    private const float Max = 1_000;
    private float _current;

    public event UnityAction Overed;
    public event UnityAction HeadShoted;

    private void Start()
    {
        _current = Max;
    }

    public void ApplyDamage(float amount)
    {
        if (_isHead)
        {
            // _current = 0;
            _current = Mathf.Clamp(_current - amount, 0, _current);
            print("headshoted");
            HeadShoted?.Invoke();
        }
        else
        {
            _current = Mathf.Clamp(_current - amount, 0, _current);
        }

        print(_current);

        if (_current == 0)
            Overed?.Invoke();
    }
}
