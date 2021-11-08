using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // track the animation component.
    // track the animation clips for fade in and out.
    // function that can recieve animation events
    // functions to play fade in and out animations

    [SerializeField] private Animation mainMenuAnimator;
    [SerializeField] private AnimationClip fadeOutAnimation;
    [SerializeField] private AnimationClip fadeInAnimation;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    private void Start()
    {
        GameManager_.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    public void OnFadeOutComplete()
    {
        OnMainMenuFadeComplete.Invoke(true);
    }

    public void OnFadeInComplete()
    {
        OnMainMenuFadeComplete.Invoke(false);
        UIManager.Instance.SetDummyCameraActive(true);
    }

    private void HandleGameStateChanged(GameManager_.GameState currentState, GameManager_.GameState previousState)
    {
        if (previousState == GameManager_.GameState.PREGAME && currentState == GameManager_.GameState.RUNNING)
        {
            FadeOut();
        }

        if (previousState != GameManager_.GameState.PREGAME && currentState == GameManager_.GameState.PREGAME)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        // if anything is currently playing "stop it"
        mainMenuAnimator.Stop();

        // Set the animation we want to play.
        mainMenuAnimator.clip = fadeInAnimation;

        // play the animation
        mainMenuAnimator.Play();
    }

    public void FadeOut()
    {
        UIManager.Instance.SetDummyCameraActive(false);

        // if anything is currently playing "stop it"
        mainMenuAnimator.Stop();

        // Set the animation we want to play.
        mainMenuAnimator.clip = fadeOutAnimation;

        // play the animation
        mainMenuAnimator.Play();
    }
}
