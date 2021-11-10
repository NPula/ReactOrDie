using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Upgrades : MonoBehaviour
{
    private CharacterStats m_playerStats;

    private void Start()
    {
        m_playerStats = GameObject.Find("Player").GetComponent<CharacterStats>();
    }

    public void UpgradeAttack()
    {
        m_playerStats.baseDamage += 2;
    }

    public void UpgradeHealth()
    {
        m_playerStats.maxHealth += 5;
        m_playerStats.health = m_playerStats.maxHealth;
    }

    public void SwitchWeapon()
    {
        Debug.Log("Switching Weapons!");
    }
}
