using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData _data;
    [SerializeField] private Clip _clip;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Collider2D _collider;

    private bool _canShoot = true;
    private bool _isReloading = false;

    private WaitForSeconds _fireRateDelay;
    private WaitForSeconds _reloadingTime;

    public WeaponData Data => _data;

    public event UnityAction<int> BulletsChanged;

    private const int SecondsInMinute = 60;

    private void Start()
    {
        _fireRateDelay = new WaitForSeconds(SecondsInMinute / _data.FireRate);
        _reloadingTime = new WaitForSeconds(_data.ReloadTime);
        _clip.Init(_data.Bullet, _data.MaxBullets);

        BulletsChanged?.Invoke(_clip.CurrentBulletsCount); 
    }

    private void ResetSettings()
    {
        StopAllCoroutines();
        _canShoot = true;
        _isReloading = false;

        BulletsChanged?.Invoke(_clip.CurrentBulletsCount);
    }

    public void TryShoot(Vector2 lookDirection)
    {
        if (!_canShoot || _isReloading)
            return;

        var bullet = _clip.TryGetBullet();

        if (bullet == null)
            return;

        float bulletRotationZ = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        bullet.gameObject.SetActive(true);
        bullet.Init(lookDirection, _shootPoint.position, _data.ShotPower, bulletRotationZ, _data.FireRange, _data.DamageReduceOverMaxDistance);
        BulletsChanged?.Invoke(_clip.CurrentBulletsCount);

        StartCoroutine(InternalReloading());
    }

    public void Reload()
    {
        if (_isReloading)
            return;

        StartCoroutine(Reloading());
    }

    private IEnumerator InternalReloading()
    {
        _canShoot = false;
        yield return _fireRateDelay;
        _canShoot = true;
    }

    private IEnumerator Reloading()
    {
        _isReloading = true;
        yield return _reloadingTime;
        _isReloading = false;

        _clip.Refresh();
        BulletsChanged?.Invoke(_clip.CurrentBulletsCount);
    }

    public void Drop()
    {
        // Vector3[] dropPath = { new Vector3(1, 1, 0), new Vector3(1, 2, 0), new Vector3(2, 1, 0), new Vector3(3, 0, 0) };
        // transform.DOLocalPath(dropPath, 1);
        // transform.DOLocalJump(transform.position + new Vector3(3, 0, 0), 2, 1, 1);
        //GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 2);

        transform.rotation = Quaternion.Euler(Vector3.zero);
        _collider.isTrigger = true;
        transform.parent = null;
        ResetSettings();
    }

    public void OnPickUp(Transform weaponPoint)
    {
        _collider.isTrigger = false;
        transform.parent = weaponPoint;
        transform.position = weaponPoint.position;
        transform.rotation = weaponPoint.rotation;
        ResetSettings();
    }
}
