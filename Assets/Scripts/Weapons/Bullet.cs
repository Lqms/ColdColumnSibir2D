using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            health.ApplyDamage(1);
            Destroy(gameObject);
        }

        if (collision.TryGetComponent(out Obstacle obstacle))
        {
            Destroy(gameObject);
        }
    }

    public void Init(Vector2 direction)
    {
        _rigidbody.velocity = direction.normalized * _speed;
    }
}
