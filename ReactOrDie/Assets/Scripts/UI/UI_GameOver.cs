using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Instance.state = GameManager.State.RUNNING;
        Debug.Log("setting scene state: " + GameManager.Instance.state);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
