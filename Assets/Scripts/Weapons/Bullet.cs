using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _damage;
    private Coroutine _coroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            health.ApplyDamage(_damage);
            print("Hitting " + _damage);
        }

        gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    public void Init(Vector2 direction, Vector3 position, float shotPower, float rotationZ, float fireRange, float damageReduceOverDistance)
    {
        _damage = shotPower;

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        transform.position = position;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Flying(direction, shotPower, fireRange, damageReduceOverDistance));
    }

    private IEnumerator Flying(Vector3 direction, float shotPower, float fireRange, float damageReduceOverMaxDistance)
    {
        Vector3 startPoint = transform.position;
        float minSpeed = 10;
        float maxSpeed = 50;
        float moveDelta = Time.deltaTime * Mathf.Clamp(shotPower, minSpeed, maxSpeed);

        while (Vector3.Distance(transform.position, startPoint) < fireRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, moveDelta); // если будут быстро пролетать то делать физикой
            float damageReducePercent = (fireRange - Vector3.Distance(transform.position, startPoint)) / fireRange;
            _damage = Mathf.Clamp(shotPower * damageReducePercent, shotPower / damageReduceOverMaxDistance, shotPower);

            yield return null;
        }

        print("Reach max range " + _damage);
        gameObject.SetActive(false);
    }
}
