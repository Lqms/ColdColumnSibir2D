using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _max;
    [SerializeField] private float _current;

    public event UnityAction Overed;

    private void Start()
    {
        _current = _max;
    }

    public void ApplyDamage(float amount)
    {
        _current = Mathf.Clamp(_current - amount, 0, _current);

        if (_current == 0)
        {
            Overed?.Invoke();
            Destroy(gameObject);
        }
    }
}
