using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    public int waveNumber = 1;
    private Spawner[] m_spawner;
    [HideInInspector] public int enemiesLeftToKill = 0;
    private bool m_canCheckEnemiesLeft = false;

    private void Start()
    {
        m_spawner = FindObjectsOfType<Spawner>();
        
        EventManager.Instance.StartListening("EnemyKilled", RemoveEnemy);
        EventManager.Instance.StartListening("WaveDoneSpawning", CheckEnemiesLeft);
    }

    private void Update()
    {
        if (GameManager.Instance.state == GameManager.State.UPGRADES)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextWave();
            }
        }
        else
        {
            if (m_canCheckEnemiesLeft)
            {
                if (enemiesLeftToKill <= 0)
                {
                    EndWave();
                }
            }
        }
    }

    private void RemoveEnemy(EventManager.EventParam evtParam)
    {
        enemiesLeftToKill--;
    }

    private void CheckEnemiesLeft(EventManager.EventParam evtParam)
    {
        m_canCheckEnemiesLeft = true;
    }

    private void EndWave()
    {
        m_canCheckEnemiesLeft = false;
        waveNumber++;
        GameManager.Instance.state = GameManager.State.UPGRADES;
    }

    private void NextWave()
    {
        EventManager.EventParam param = new EventManager.EventParam();
        EventManager.TriggerEvent("OnNextWave", param);
        GameManager.Instance.state = GameManager.State.RUNNING;
    }
}
