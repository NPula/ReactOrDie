using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 m_direction;
    private float m_speed = 20f;
    private float m_damage = 0f;

    // Sets where to destroy the bullet objects after being fired.
    private float m_boundsX = 16;
    private float m_boundsY = 10;

    void Update()
    {
        transform.Translate(m_direction * m_speed * Time.deltaTime);
        DestroyOutOfBounds();
    }

    public void SetDirection(Vector2 direction)
    {
        m_direction = direction;
    }

    public void SetSpeed(float speed)
    {
        m_speed = speed;
    }

    public void SetDamage(float dmg)
    {
        m_damage = dmg;
    }

    public float GetDamage()
    {
        return m_damage;
    }

    private void DestroyOutOfBounds()
    {
        if (transform.position.y > m_boundsY || transform.position.y < -m_boundsY ||
            transform.position.x > m_boundsX || transform.position.x < -m_boundsX)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
