using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum State
    {
        RUNNING,
        PAUSED,
        UPGRADES,
        GAMEOVER,
        RESTART,
        QUIT
    }
    public State state;

    private void Start()
    {
        DontDestroyOnLoad(this);
        state = State.RUNNING;
    }

    public void Update()
    {
        Debug.Log("Game State: " + state);
        if (Input.GetKeyDown(KeyCode.Escape) && (state == State.RUNNING || state == State.PAUSED))
        {
            state = (state == State.RUNNING) ? State.PAUSED : State.RUNNING;
        }

        if (state != State.RUNNING)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (state == State.RESTART)
        {
            //EventManager.Instance.dictionary.Clear();
            EventManager.TriggerEvent("Reset", new EventManager.EventParam());
            SceneManager.LoadScene(1);
        }

        if (state == State.QUIT)
        {
            Application.Quit();
        }
    }
}
