using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyStateMachine))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private EnemyStateMachine _stateMachine;
    [SerializeField] private Health _health;
    [SerializeField] private NavMeshAgent _agent;

    [Header("Other objects")]
    [SerializeField] private Weapon _weapon;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Player _player;
    [SerializeField] private DetectionSystem _detectionSystem;

    private void OnEnable()
    {
        _health.Overed += OnHealthOvered;
        _detectionSystem.PlayerDetected += OnPlayerDetected;
    }

    private void OnDisable()
    {
        _health.Overed += OnHealthOvered;
        _detectionSystem.PlayerDetected -= OnPlayerDetected;
    }

    private void Awake()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _stateMachine.Init(_agent, _spriteRenderer, _player);
    }

    private void OnHealthOvered()
    {
        _weapon.Drop();
        Destroy(gameObject);
    }

    private void OnPlayerDetected()
    {
        Destroy(_detectionSystem.gameObject);
        _stateMachine.SwitchState(States.Combat);
    }
}
