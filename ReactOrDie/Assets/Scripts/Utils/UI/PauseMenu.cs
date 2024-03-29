﻿using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button QuitButton;

    void Start()
    {
        ResumeButton.onClick.AddListener(HandleResumeClicked);
        RestartButton.onClick.AddListener(HandleRestartClicked);
        QuitButton.onClick.AddListener(HandleQuitClicked);
    }

    void HandleResumeClicked()
    {
        GameManager_.Instance.TogglePause();
    }

    void HandleRestartClicked()
    {
        GameManager_.Instance.RestartGame();
    }

    void HandleQuitClicked()
    {
        GameManager_.Instance.QuitGame();
    }
}
