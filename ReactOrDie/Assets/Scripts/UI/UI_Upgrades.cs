using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Upgrades : MonoBehaviour
{
    private CharacterStats m_playerStats;
    [SerializeField] private GameObject[] m_weapons;

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
        foreach (GameObject weapon in m_weapons)
        {
            weapon.SetActive(false);
        }

        //m_playerStats.ChangeWeapon();
    }

    public void SwapPistol()
    {
        SwitchWeapon();
        m_weapons[0].SetActive(true);
        m_playerStats.ChangeWeapon(m_weapons[0].GetComponent<Weapon>());
    }

    public void SwapShotgun()
    {
        SwitchWeapon();
        m_weapons[1].SetActive(true);
        m_playerStats.ChangeWeapon(m_weapons[1].GetComponent<Weapon>());
    }
}
