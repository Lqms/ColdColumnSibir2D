using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private ScoreHandler _scoreHandler;

    [Header("UI")]
    [SerializeField] private Image _panel;
    [SerializeField] private Text _header;
    [SerializeField] private Text _killScore;
    [SerializeField] private Text _accuracyScore;
    [SerializeField] private Text _headshotsScore;
    [SerializeField] private Text _timeScore;
    [SerializeField] private Text _pressAnyButton;

    [Header("Texts")]
    [SerializeField] Text[] _texts;

    private bool _scoreAnimationIsOver;

    private void OnEnable()
    {
        _car.PlayerSatInCar += OnPlayerSatInCar;
    }

    private void OnDisable()
    {
        _car.PlayerSatInCar -= OnPlayerSatInCar;
    }

    private void Start()
    {
        _panel.color = new Color(0, 0, 0, 0);

        foreach (var text in _texts)
            text.gameObject.SetActive(false);

        _panel.gameObject.SetActive(false);
    }

    private void OnPlayerSatInCar()
    {
        StartCoroutine(Fading());
    }

    private IEnumerator TextScoreAnimating(Text text, float score, string textValue, float animationSpeed)
    {
        text.gameObject.SetActive(true);
        _scoreAnimationIsOver = false;
        float tempScore = 0;
        text.text = textValue + (int)tempScore;
        float startTime = Time.time;

        while (tempScore < score)
        {
            if (Input.anyKeyDown || score == 0)
                break;

            tempScore = Mathf.Clamp(tempScore + Mathf.Pow((Time.time - startTime), 2), tempScore, score);
            text.text = textValue + (int)tempScore;

            yield return new WaitForSeconds(Time.deltaTime * animationSpeed);
        }

        text.text = textValue + (int)score;
        _scoreAnimationIsOver = true;
    }

    private IEnumerator Fading()
    {
        float alpha = 0;
        float fadeSpeed = 2;

        _panel.gameObject.SetActive(true);
        _panel.color = new Color(0, 0, 0, alpha);

        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            _panel.color = new Color(0, 0, 0, alpha);

            yield return null;
        }

        IEnumerator[] coroutines = {
            TextScoreAnimating(_killScore, _scoreHandler.KillScoreBonus, "Kill Score Bonus: ", 0.02f),
            TextScoreAnimating(_accuracyScore, _scoreHandler.AccuracyScoreBonus, "Accuracy Bonus: ", 0.02f),
            TextScoreAnimating(_headshotsScore, _scoreHandler.HeadshotsScoreBonus, "Headshots Bonus: ", 0.02f),
            TextScoreAnimating(_timeScore, _scoreHandler.TimeScoreBonus, "Time Bonus: ", 0.02f)
            };

        for (int i = 0; i < coroutines.Length; i++)
        {
            StartCoroutine(coroutines[i]);

            while (_scoreAnimationIsOver == false)
            {
                yield return null;
            }
        }

        StartCoroutine(WaitingForInput());
    }

    private IEnumerator WaitingForInput()
    {
        while (true)
        {
            yield return null;

            if (Input.anyKeyDown)
            {
                LoadNextLevel();
            }
        }
    }

    private static void LoadNextLevel()
    {
        print("уровень завершён");
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int mainMenuSceneIndex = 0;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(mainMenuSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
