using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Clip : MonoBehaviour
{
    private Bullet _bullet;
    private int _capacity;

    private List<Bullet> _bullets = new List<Bullet>();
    private int _currentBulletNumber = 0;

    public int CurrentBulletsCount => _capacity - _currentBulletNumber;

    private void OnDisable()
    {
        _bullets.ForEach(b => Destroy(b.gameObject));
        _bullets.Clear();
    }

    public Bullet TryGetBullet()
    {
        if (_currentBulletNumber >= _capacity)
            return null;

        var bullet = _bullets.FirstOrDefault(b => b.gameObject.activeSelf == false);
        _currentBulletNumber++;

        if (bullet == null)
        {
            bullet = Instantiate(_bullet);
            _bullets.Add(bullet);
        }

        return bullet;
    }

    public void Refresh()
    {
        _currentBulletNumber = 0;
    }

    public void Init(Bullet bullet, int maxBullets)
    {
        _bullet = bullet;
        _capacity = maxBullets;
    }
}
