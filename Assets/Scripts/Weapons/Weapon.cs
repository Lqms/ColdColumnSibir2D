using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData _data;
    [SerializeField] private Clip _clip;
    [SerializeField] private Transform _shootPoint;

    private bool _canShoot = true;
    private bool _isReloading = false;

    private SpriteRenderer _spriteRenderer;
    private WaitForSeconds _fireRateDelay;
    private WaitForSeconds _reloadingTime;

    public WeaponData Data => _data;

    public event UnityAction<int, int> BulletsChanged;

    private const int SecondsInMinute = 60;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        SetNewData(_data);
    }

    public void SetNewData(WeaponData newData)
    {
        ResetSettings();

        _data = newData;
        _fireRateDelay = new WaitForSeconds(SecondsInMinute / _data.FireRate);
        _reloadingTime = new WaitForSeconds(_data.ReloadTime);
        _spriteRenderer.sprite = newData.Sprite;
        _clip.Init(newData.Bullet, newData.MaxBullets);

        BulletsChanged?.Invoke(_clip.CurrentBulletsCount, _clip.MaxBullets);
    }

    private void ResetSettings()
    {
        StopAllCoroutines();
        _canShoot = true;
        _isReloading = false;
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
        BulletsChanged?.Invoke(_clip.CurrentBulletsCount, _clip.MaxBullets);

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
        BulletsChanged?.Invoke(_clip.CurrentBulletsCount, _clip.MaxBullets);
    }
}
