using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float m_speed = 1f;
    public Weapon currentWeapon;

    public CharacterStats stats;
    private Transform m_target = null;
    
    private EventManager.EventParam m_eventParams;
    [SerializeField] Animator m_animation;

    private void Awake()
    {
        EventManager.Instance.StartListening("EnemyKilled", OnEnemyDeath);
    }

    private void Start()
    {
        m_target = GameObject.Find("Player").transform;

        // Events parameters for enemies
        m_eventParams = new EventManager.EventParam();
        m_eventParams.param5 = this.gameObject;

    }

    private void Update()
    {
        if (m_target != null)
        {
            // Enemy should always be moving. At least for now.(Change later)
            m_animation.SetBool("isMoving", true);

            // move towards the player
            Vector2 directionToMove = (m_target.transform.position - transform.position).normalized;
            Flip(directionToMove);
            transform.Translate(directionToMove * m_speed * Time.deltaTime);
        }

        if (stats.health <= 0)
        {
            EventManager.TriggerEvent("EnemyKilled", m_eventParams);
        }
    }

    private void Flip(Vector2 direction)
    {
        Vector2 characterScale = transform.localScale;
        if (direction.x > 0)
        {
            characterScale.x = 1;
        }
        else
        {
            characterScale.x = -1;
        }

        transform.localScale = characterScale;
    }

    private void OnEnemyDeath(EventManager.EventParam eventParams)
    {
            m_target.GetComponent<CharacterStats>().AddScraps(stats.scraps);
            Destroy(eventParams.param5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            float dmg = collision.GetComponent<Bullet>().GetDamage();
            stats.TakeDamage(dmg);
        }
    }
}
