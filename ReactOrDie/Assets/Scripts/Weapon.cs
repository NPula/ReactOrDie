using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float dmg = 0f;
    public float camShakeAmount = .1f;
    protected CameraShake m_camShake;


    private void Start()
    {
        m_camShake = FindObjectOfType<CameraShake>();
        m_camShake.shakeAmount = camShakeAmount;
    }
    public virtual void Fire(Vector2 direction)
    {

    }
}
