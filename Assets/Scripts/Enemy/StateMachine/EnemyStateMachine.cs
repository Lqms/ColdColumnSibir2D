using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _startState;

    [Header("States")]
    [SerializeField] private CombatState _combat;
    [SerializeField] private IdleState _idle;
    [SerializeField] private PatrolState _patrol;

    private State[] _states;

    public void Init(NavMeshAgent agent, SpriteRenderer spriteRenderer, Player player)
    {
        _states = new State[] { _combat, _idle, _patrol};

        foreach (var state in _states)
        {
            state.Init(agent, spriteRenderer, player);
            state.enabled = false;
        }

        _startState.enabled = true;
    }
}
