using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum State
    {
        RUNNING,
        PAUSED
    }
    public State state;

    [SerializeField] public List<GameObject> allEnemies = new List<GameObject>();
    public WaveManager waveManager;
    public PlayerController player;

    public delegate void OnWaveChange();
    public static event OnWaveChange OnWaveChanged;


    private void Start()
    {
        DontDestroyOnLoad(this);
        state = State.RUNNING;
    }

    public void Update()
    {
        Debug.Log("Game State: " + state);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            state = (state == State.RUNNING) ? State.PAUSED : State.RUNNING;
        }

        if (state == State.PAUSED)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        // On starting a new wave
        if (waveManager.onChange && GameManager.Instance.state != GameManager.State.PAUSED)
        {
            if (OnWaveChanged != null)
            {
                OnWaveChanged();
                waveManager.onChange = false;
            }
        }
    }
}
