using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Clip : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    private List<Bullet> _bullets = new List<Bullet>();

    public Bullet GetBullet()
    {
        var bullet = _bullets.FirstOrDefault(b => b.gameObject.activeSelf == false);

        if (bullet == null)
        {
            bullet = Instantiate(_bullet);
            _bullets.Add(bullet);
        }

        bullet.gameObject.SetActive(true);

        return bullet;
    }
}
