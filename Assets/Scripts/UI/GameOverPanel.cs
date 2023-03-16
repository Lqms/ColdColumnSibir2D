using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private Image _panel;

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

        while (true)
        {
            yield return null;

            if (Input.anyKeyDown)
            {
                Application.Quit();
            }
        }
    }
}
