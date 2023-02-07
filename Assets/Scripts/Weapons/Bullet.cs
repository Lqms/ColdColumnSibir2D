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
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            health.ApplyDamage(1);
            gameObject.SetActive(false);
        }

        if (collision.TryGetComponent(out Obstacle obstacle))
        {
            gameObject.SetActive(false);
        }
    }

    public void Init(Vector2 direction, Vector3 position, float rotationZ)
    {
        _rigidbody.velocity = Vector2.zero;
        transform.position = position;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        _rigidbody.velocity = direction.normalized * _speed;
    }
}
