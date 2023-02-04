using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Health _health;
    [SerializeField] private Weapon _weapon;

    [Header("States")]
    [SerializeField] private CombatState _combat;
    [SerializeField] private PatrolState _patrol;
    [SerializeField] private IdleState _idle;

    private void OnEnable()
    {
        _health.Overed += OnHealthOvered;
    }

    private void OnDisable()
    {
        _health.Overed += OnHealthOvered;
    }

    private void Update()
    {
        
    }

    private void OnHealthOvered()
    {
        _weapon.Drop();
        Destroy(gameObject);
    }
}
