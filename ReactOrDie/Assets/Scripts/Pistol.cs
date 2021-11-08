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
            bullet.GetComponent<Bullet>().SetDirection(direction);
            bullet.GetComponent<Bullet>().SetSpeed(m_bulletSpeed);
        }
    }
}
