using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Clip : MonoBehaviour
{
    private Bullet _bullet;
    private int _capacity;
    private int _currentBulletNumber;
    private List<Bullet> _bullets;

    public int MaxBullets => _capacity;
    public int CurrentBulletsCount => _capacity - _currentBulletNumber;

    private void Start()
    {
        _bullets = new List<Bullet>();
        _currentBulletNumber = 0;
    }

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
            return bullet;
        }
        else
        {
            return bullet;
        }
    }

    public void Refresh()
    {
        _currentBulletNumber = 0;
    }

    public void Init(Bullet bullet, int maxBullets)
    {
        _bullet = bullet;
        _capacity = maxBullets;
        _currentBulletNumber = 0;
    }
}
