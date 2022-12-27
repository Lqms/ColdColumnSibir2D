using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create New Weapon Data", fileName = "New Weapon Data", order = 54)]
public class WeaponData : ScriptableObject
{
    [SerializeField] private float _shotPower;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _fireRange;
    [SerializeField] private float _damageReduceOverMaxDistance;
    [SerializeField] private int _maxBullets;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Bullet _bullet;

    public float ShotPower => _shotPower;
    public float FireRate => _fireRate;
    public float ReloadTime => _reloadTime;
    public float FireRange => _fireRange;
    public float DamageReduceOverMaxDistance => _damageReduceOverMaxDistance;
    public int MaxBullets => _maxBullets;
    public Sprite Sprite => _sprite;
    public Bullet Bullet => _bullet;
}
