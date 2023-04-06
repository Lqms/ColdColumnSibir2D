using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private ScoreHandler _scoreHandler;

    [Header("UI")]
    [SerializeField] private Image _panel;
    [SerializeField] private Text _header;
    [SerializeField] private Text _killScore;
    [SerializeField] private Text _accuracy;
    [SerializeField] private Text _headshots;
    [SerializeField] private Text _timeElapsed;
    [SerializeField] private Text _pressAnyButton;

    [Header("Texts")]
    [SerializeField] Text[] _texts;

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

        _killScore.text = "Kill Score: " + _scoreHandler.KillScore.ToString();
        _accuracy.text = $"Accuracy: {Mathf.Ceil(_scoreHandler.Accuracy)}%";
        _headshots.text = "Headshots: " + _scoreHandler.HeadShotsCounter.ToString();
        _timeElapsed.text = "Time: " + _scoreHandler.TimeElapsedText;

        foreach (var text in _texts)
        {
            yield return new WaitForSeconds(0.5f);
            text.gameObject.SetActive(true);
        }


        while (true)
        {
            yield return null;

            if (Input.anyKeyDown)
            {
                print("уровень завершён");
            }
        }
    }
}
