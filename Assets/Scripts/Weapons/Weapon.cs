using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Clip))]
[RequireComponent(typeof(Collider2D))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private Clip _clip;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _fireRate;
    [SerializeField] private int _bulletsCount;
    [SerializeField] private AudioClip _shootSFX;

    private Coroutine _internalReloadingCoroutine;
    private WaitForSeconds _fireRateDelay;

    public int BulletsCount => _bulletsCount;
    public AudioClip ShootSFX => _shootSFX;

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
        var bullet = _clip.GetBullet();
        bullet.Init(lookDirection, _shootPoint.position, bulletRotationZ);

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
