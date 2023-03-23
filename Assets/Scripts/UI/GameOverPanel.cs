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
    [SerializeField] private Text _pressAnyButton;

    private void OnEnable()
    {
        _car.PlayerSatInCar += OnPlayerSatInCar;
    }

    private void OnDisable()
    {
        _car.PlayerSatInCar -= OnPlayerSatInCar;
    }

    private void OnPlayerSatInCar()
    {
        _panel.gameObject.SetActive(true);

        StartCoroutine(Fading());
    }

    private IEnumerator Fading()
    {
        float alpha = 0;
        float fadeSpeed = 2;
        _panel.color = new Color(0, 0, 0, alpha);

        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            _panel.color = new Color(0, 0, 0, alpha);

            yield return null;
        }

        // анимированный UI с итогами уровня, который можно скипнуть и в конце подтверждение нажатием любой клавиши для перехода на след. уровень

        Text[] uiElements = { _header, _killScore, _pressAnyButton};
        _killScore.text = "Kill Score: " + _scoreHandler.KillScore.ToString();

        foreach (var element in uiElements)
        {
            yield return new WaitForSeconds(0.5f);
            element.gameObject.SetActive(true);
        }

        print(_scoreHandler.Accuracy);

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
