using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float baseDamage = 2;
    public float health = 10f;
    public float maxHealth = 10f;
    public float scraps = 0f; // money in the game for purchasing/creating upgrades. might move this elsewhere

    public void TakeDamage(float damage)
    {
        if (health - damage >= 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }
    }

    public void AddScraps(float amount)
    {
        scraps += amount;
    }

    public void AddHealth(float amount)
    {
        if (health + amount <= maxHealth)
        {
            health += amount;
        }
        else
        {
            health = maxHealth;
        }
    }

    public float GetDamage(Weapon weapon)
    {
        return baseDamage + weapon.dmg;
    }

    public void OnDeath()
    {
        if (health <= 0)
        {
        }
    }
}
