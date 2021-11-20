using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Instance.state = GameManager.State.RESTART;
        //SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        GameManager.Instance.state = GameManager.State.QUIT;
        //Application.Quit();
    }
}
