using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private GameObject m_spawnObject;
    [SerializeField] private float m_bulletSpeed;

    public override void Fire(Vector2 direction)
    {
        if (m_spawnObject != null)
        {
            SpawnBullet(direction);
            SpawnBullet(RotateDirection(direction, 15));
            SpawnBullet(RotateDirection(direction, -15));

            if (m_camShake != null)
            {
                m_camShake.shakeDuration = .1f;
            }
        }
    }

    public void SpawnBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(m_spawnObject, transform.position, Quaternion.identity);
        if (bullet != null)
        {
            Bullet b = bullet.GetComponent<Bullet>();
            Debug.Log("Bullet Direction: " + direction);
            b.SetDirection(direction);
            b.SetSpeed(m_bulletSpeed);
            b.SetDamage(GameObject.Find("Player").GetComponent<CharacterStats>().GetDamage());
        }
    }

    public Vector2 RotateDirection(Vector2 direction, float angleInDegrees)
    {
        float sin = Mathf.Sin(angleInDegrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(angleInDegrees * Mathf.Deg2Rad);

        float tx = direction.x;
        float ty = direction.y;
        direction.x = (cos * tx) - (sin * ty);
        direction.y = (sin * tx) + (cos * ty);
        return direction;
    }
}
