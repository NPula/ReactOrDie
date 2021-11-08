using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float m_speed = 1f;
    private Transform m_target = null;

    private void Start()
    {
        m_target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (m_target != null)
        {
            // move towards the player
            Vector2 directionToMove = (m_target.transform.position - transform.position).normalized;
            transform.Translate(directionToMove * m_speed * Time.deltaTime);
        }
    }
}
