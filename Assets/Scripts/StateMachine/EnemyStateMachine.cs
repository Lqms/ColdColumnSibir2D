using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _current;

    private void Start()
    {
        _current.Enter();
    }

    private void Update()
    {
        _current.Update();
    }
}
