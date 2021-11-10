using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_enemies; // list of all possible enemies.
    [SerializeField] private GameObject m_parentObject;
    [SerializeField] private float m_timeBetweenSpawn = 1f;

    private List<GameObject> allEnemies = new List<GameObject>();
    private int numOfEnemiesToSpawn = 1;

    private void Start()
    {
        StartCoroutine(SpawnEnemies(1));
    }

    public IEnumerator SpawnEnemies(int spawnNumber)
    {
        for (int i = 0; i < numOfEnemiesToSpawn * spawnNumber; i++)
        {
            // add a random offset to the spawned enemies.
            Vector3 randOffset = new Vector3(Random.Range(0, 5), Random.Range(0, 5), 0);

            // Get a random enemy index to spawn.
            int randSelection = Random.Range(0, m_enemies.Count);
            GameObject go = Instantiate(m_enemies[randSelection], transform.position + randOffset, Quaternion.identity);
            if (m_parentObject != null)
            {
                go.transform.SetParent(go.transform);
            }

            GameManager.Instance.allEnemies.Add(go);
            
            yield return new WaitForSeconds(m_timeBetweenSpawn);
        }
    }

    public void SetNumberOfEnemies(int numOfEnemies)
    {
        numOfEnemiesToSpawn = numOfEnemies;
    }

    public void SpawnWave(int spawnNumber)
    {
        Debug.Log("called: " + spawnNumber);
        StartCoroutine(SpawnEnemies(spawnNumber));
    }
}
