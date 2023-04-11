using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _continueButton.onClick.AddListener(OnContinueButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClicked);
        _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        print("start");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnContinueButtonClicked()
    {
        print("Continue");
    }

    private void OnSettingsButtonClicked()
    {
        print("settings");
    }

    private void OnExitButtonClicked()
    {
        print("exit");
        Application.Quit();
    }
}
