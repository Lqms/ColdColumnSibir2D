using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private int _enemyKillBounty = 100;
    [SerializeField] private PlayerCombat _playerCombat;
    [SerializeField] private float _killStreakTimer = 10;

    private int _killStreakCounter = 1;
    private int _killScore;
    private Coroutine _coroutine;
    private float _enemiesCount;
    private float _playerShootsCount;

    public int KillScore => _killScore;
    public float Accuracy { get; private set; }

    private void OnEnable()
    {
        Enemy.Died += OnEnemyDied;
        _playerCombat.Shooted += OnPlayerShooted;
    }

    private void OnDisable()
    {
        Enemy.Died -= OnEnemyDied;
        _playerCombat.Shooted -= OnPlayerShooted;
    }

    private void Start()
    {
        _enemiesCount = FindObjectsOfType<Enemy>().Length;
        // print(_enemiesCount);
    }

    private void OnEnemyDied()
    {
        _killScore += _enemyKillBounty * _killStreakCounter;
        _killStreakCounter++;
        // print(_killStreakCounter);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(CountingKillStreakTimer());
    }

    private IEnumerator CountingKillStreakTimer()
    {
        yield return new WaitForSeconds(_killStreakTimer);

        _killStreakCounter = 1;
        // print(_killStreakCounter);
        _coroutine = null;
    }

    private void OnPlayerShooted()
    {
        _playerShootsCount++;
        // print(_playerShootsCount);
        Accuracy = (_enemiesCount / _playerShootsCount) * 100;
        print(Accuracy);
    }
}
