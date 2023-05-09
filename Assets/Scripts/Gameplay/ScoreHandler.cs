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
    private float _killedEnemiesCount;
    private float _playerShootsCount;
    private int _headShotsCounter;
    private float _timeElapsed = 0;

    private Coroutine _coroutine;

    public int KillScore => _killScore;
    public int HeadShotsCounter => _headShotsCounter * HeadshotsScoreMultiplier; // тут тоже небольшую систему честную за %-ность хедов
    public float Accuracy => (_killedEnemiesCount / _playerShootsCount) * AccuracyScoreMultiplier;
    public float TimeElapsed => Mathf.Clamp(1000 - _timeElapsed, 0, 1000); // тут систему с 2 сериалайзд филд полями

    // public string TimeElapsedText => GetElapsedTimeText(_timeElapsed);

    private const int AccuracyScoreMultiplier = 1000;
    private const int HeadshotsScoreMultiplier = 50;

    private void OnEnable()
    {
        Enemy.Died += OnEnemyDied;
        Enemy.HeadShoted += OnHeadShoted;
        _playerCombat.Shooted += OnPlayerShooted;
    }

    private void OnDisable()
    {
        Enemy.Died -= OnEnemyDied;
        Enemy.HeadShoted -= OnHeadShoted;
        _playerCombat.Shooted -= OnPlayerShooted;
    }

    private void Update()
    {
        _timeElapsed += Time.deltaTime;
    }

    /*
    private string GetElapsedTimeText(float elapsedTime)
    {
        int timeElapsed = (int)elapsedTime;

        int minutes = timeElapsed / 60;
        int seconds = timeElapsed - minutes * 60;

        string secondsText = seconds.ToString();
        string minutesText = minutes.ToString();

        if (secondsText.Length == 1)
            secondsText = "0" + secondsText;

        if (minutesText.Length == 1)
            minutesText = "0" + minutesText;

        return $"{minutesText}m {secondsText}s";
    }
    */

    private void OnEnemyDied(Enemy enemy)
    {
        _killScore += _enemyKillBounty * _killStreakCounter;
        _killStreakCounter++;
        _killedEnemiesCount++;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(CountingKillStreakTimer());
    }

    private IEnumerator CountingKillStreakTimer()
    {
        yield return new WaitForSeconds(_killStreakTimer);

        _killStreakCounter = 1;
        _coroutine = null;
    }

    private void OnPlayerShooted()
    {
        _playerShootsCount++;
    }

    private void OnHeadShoted()
    {
        _headShotsCounter++;
        print(_headShotsCounter);
    }
}
