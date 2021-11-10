using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int waveNumber = 1;
    [SerializeField] private List<Spawner> m_spawner;
    public bool onChange = false;

    private void Start()
    {
        GameManager.OnWaveChanged += NextWave;
    }

    private void Update()
    {
        if (GameManager.Instance.allEnemies.Count <= 0 && GameManager.Instance.state != GameManager.State.PAUSED)
        {
            onChange = true;
            
            // Stop the coroutine for each spawner before subscribing a new 
            // coroutine in the spawner class.
            foreach (Spawner spawner in m_spawner)
            {
                StopCoroutine(spawner.SpawnEnemies(waveNumber));
            }

            Debug.Log("asdfadfasf");
            waveNumber++;
            GameManager.Instance.state = GameManager.State.PAUSED;
            //GameManager.OnWaveChanged += NextWave;
        }
    }

    private void NextWave()
    {
        Debug.Log("Next waves added");
        foreach (Spawner spawner in m_spawner)
        {
            spawner.SpawnWave(waveNumber);
        }
    }
}
