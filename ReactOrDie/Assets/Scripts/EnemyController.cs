using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float m_speed = 1f;
    public Weapon currentWeapon;

    public CharacterStats stats;
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

        if (stats.health <= 0)
        {
            m_target.GetComponent<CharacterStats>().AddScraps(stats.scraps);
            Debug.Log("Scraps: " + m_target.GetComponent<CharacterStats>().scraps);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy Attacked: ");
        if (collision.CompareTag("Bullet"))
        {
            float dmg = collision.GetComponent<Bullet>().GetDamage();
            stats.TakeDamage(dmg);
            Debug.Log("Enemy Health: " + stats.health);
        }
    }
}
