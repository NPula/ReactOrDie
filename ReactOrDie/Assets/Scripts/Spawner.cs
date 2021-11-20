using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_enemies; // list of all possible enemies.
    [SerializeField] private GameObject m_parentObject;  // to keep hierarchy clean
    [SerializeField] private float m_timeBetweenSpawn = 1f;

    [HideInInspector] public List<GameObject> allEnemies = new List<GameObject>();
    private int m_numOfEnemiesToSpawn = 1;
    private bool m_hasSpawnedEverything = false;

    private EventManager.EventParam m_eventParams;

    private void Start()
    {
        EventManager.Instance.StartListening("OnWaveEnd", StopSpawning);
        EventManager.Instance.StartListening("OnNextWave", SpawnWave);
        EventManager.Instance.StartListening("Reset", GameReset);

        m_eventParams = new EventManager.EventParam();
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < m_numOfEnemiesToSpawn * WaveManager.Instance.waveNumber; i++)
        {
            // add a random offset to the position of spawned enemies.
            //Vector3 randOffset = new Vector3(Random.Range(0, 5), Random.Range(0, 5), 0);

            // get a random enemy index to spawn if there are multiple enemy types.
            int randSelection = Random.Range(0, m_enemies.Count);

            // spawn enemies
            GameObject go = Instantiate(m_enemies[randSelection], transform.position/* + randOffset*/, Quaternion.identity);
            if (m_parentObject != null)
            {
                go.transform.SetParent(go.transform);
            }

            WaveManager.Instance.enemiesLeftToKill++;

            yield return new WaitForSeconds(m_timeBetweenSpawn);
        }

        // Trigger after all enemies has spawned.
        EventManager.TriggerEvent("WaveDoneSpawning", m_eventParams);
    }

    public bool HasFinishedSpawning()
    {
        return m_hasSpawnedEverything;
    }

    public void SetNumberOfEnemies(int numOfEnemies)
    {
        m_numOfEnemiesToSpawn = numOfEnemies;
    }

    public void StopSpawning(EventManager.EventParam param)
    {
        m_hasSpawnedEverything = false;
        m_numOfEnemiesToSpawn++;
    }

    public void SpawnWave(EventManager.EventParam param)
    {
        m_hasSpawnedEverything = false;
        StartCoroutine(SpawnEnemies());
    }

    private void GameReset(EventManager.EventParam param)
    {
        //EventManager.Instance.StopListening("OnWaveEnd", StopSpawning);
        //EventManager.Instance.StopListening("OnNextWave", SpawnWave);
    }
}
