using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Health _health;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private NavMeshAgent _agent;
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

    private void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
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
