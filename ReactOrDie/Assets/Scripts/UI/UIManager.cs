using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private Camera dummyCamera;
    [SerializeField] private PauseMenu pauseMenu;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    private void Start()
    {
        mainMenu.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
        GameManager_.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameManager_.GameState currentState, GameManager_.GameState previousState)
    {
        pauseMenu.gameObject.SetActive(currentState == GameManager_.GameState.PAUSED);
    }

    private void HandleMainMenuFadeComplete(bool fadeOut)
    {
        OnMainMenuFadeComplete.Invoke(fadeOut);
    }

    private void Update()
    {
        if (GameManager_.Instance.CurrentGameState != GameManager_.GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && dummyCamera.isActiveAndEnabled)
        {
            GameManager_.Instance.StartGame();
        }
    }

    public void SetDummyCameraActive(bool active)
    {
        dummyCamera.gameObject.SetActive(active);
    }
}
