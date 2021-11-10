using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private GameObject m_spawnObject;
    [SerializeField] private float m_bulletSpeed;

    public override void Fire(Vector2 direction)
    {
        if (m_spawnObject != null)
        {
            GameObject bullet = Instantiate(m_spawnObject, transform.position, Quaternion.identity);
            Bullet b = bullet.GetComponent<Bullet>();
            b.SetDirection(direction);
            b.SetSpeed(m_bulletSpeed);
            b.SetDamage(GameObject.Find("Player").GetComponent<CharacterStats>().GetDamage());

            if (m_camShake != null)
            {
                m_camShake.shakeDuration = .1f;
            }
        }
    }
}
