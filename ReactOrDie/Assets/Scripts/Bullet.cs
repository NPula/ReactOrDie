using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 m_direction;
    private float m_speed = 20f;
    private float m_damage = 0f;

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
        if (transform.position.y > 6  || transform.position.y < -6 ||
            transform.position.x > 10 || transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
