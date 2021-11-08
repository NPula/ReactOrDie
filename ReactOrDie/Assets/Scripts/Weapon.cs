using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float m_dmg;

    public virtual void Fire(Vector2 direction)
    {

    }
}
