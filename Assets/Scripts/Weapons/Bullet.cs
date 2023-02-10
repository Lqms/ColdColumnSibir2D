using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;

    private Coroutine _coroutine;

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

    public void Init(Vector2 direction, Vector3 position, float rotationZ, float fireRange)
    {
        _rigidbody.velocity = Vector2.zero;
        transform.position = position;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        _rigidbody.velocity = direction.normalized * _speed;
        
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Flying(position, fireRange));
    }

    private IEnumerator Flying(Vector2 startPosition, float maxFlyDistance)
    {
        while (Vector2.Distance(startPosition, transform.position) < maxFlyDistance)
            yield return null;

        _coroutine = null;
        gameObject.SetActive(false);
    }
}
