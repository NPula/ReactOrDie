using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_enemies;
    [SerializeField] private GameObject m_parentObject;
    [SerializeField] private float m_timeBetweenSpawn = 1f;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < m_enemies.Count; i++)
        {
            // add a random offset to the spawned enemies.
            Vector3 randOffset = new Vector3(Random.Range(0, 5), Random.Range(0, 5), 0);

            GameObject go = Instantiate(m_enemies[i], transform.position + randOffset, Quaternion.identity);
            if (m_parentObject != null)
            {
                go.transform.SetParent(go.transform);
            }

            yield return new WaitForSeconds(m_timeBetweenSpawn);
        }
    }
}
