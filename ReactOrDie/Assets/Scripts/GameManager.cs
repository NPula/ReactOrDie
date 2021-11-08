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

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }
}
