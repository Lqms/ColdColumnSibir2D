using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _fireRate;
    [SerializeField] private int _bulletsCount;

    private Coroutine _internalReloadingCoroutine;
    private WaitForSeconds _fireRateDelay;

    public int BulletsCount => _bulletsCount;

    private const int SecondsInMinutes = 60;

    private void Start()
    {
        _fireRateDelay = new WaitForSeconds(SecondsInMinutes / _fireRate);
    }

    private void OnValidate()
    {
        _fireRateDelay = new WaitForSeconds(SecondsInMinutes / _fireRate);
    }

    public bool TryShoot(Vector2 lookDirection)
    {
        if (_internalReloadingCoroutine != null || _bulletsCount <= 0)
            return false;

        float bulletRotationZ = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        var bullet = Instantiate(_bullet, _shootPoint.position, Quaternion.Euler(0, 0, bulletRotationZ));
        bullet.Init(lookDirection);

        _bulletsCount--;
        _internalReloadingCoroutine = StartCoroutine(InternalReloading());

        return true;
    }

    private IEnumerator InternalReloading()
    {
        yield return _fireRateDelay;
        _internalReloadingCoroutine = null;
    }

    public void Drop()
    {
        transform.parent = null;
        _collider.isTrigger = true;
        transform.eulerAngles = Vector3.zero;
    }

    public void OnPickUp(Transform weaponPoint)
    {
        _collider.isTrigger = false;
        transform.parent = weaponPoint;
        transform.position = weaponPoint.position;
        transform.eulerAngles = weaponPoint.eulerAngles;
    }
}
