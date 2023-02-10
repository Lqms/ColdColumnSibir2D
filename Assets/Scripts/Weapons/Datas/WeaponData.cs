using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create New Weapon Data", fileName = "New Weapon Data", order = 54)]
public class WeaponData : ScriptableObject
{
    [SerializeField] private float _fireRate;
    [SerializeField] private float _fireRange;
    [SerializeField] private AudioClip _shootSFX;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Clip _clip;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private int _maxBullets;

    public float FireRate => _fireRate;
    public float FireRange => _fireRange;
    public AudioClip ShootSFX => _shootSFX;
    public Sprite Sprite => _sprite;
    public Clip Clip => _clip;
    public Bullet Bullet => _bullet;
    public int MaxBullets => _maxBullets;
}
