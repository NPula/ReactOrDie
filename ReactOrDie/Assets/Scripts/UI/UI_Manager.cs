using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject upgradeMenu;

    private void Start()
    {
        upgradeMenu.SetActive(false);
    }

    private void Update()
    {
        switch (GameManager.Instance.state)
        {
            case GameManager.State.RUNNING:
                upgradeMenu.SetActive(false);
                break;
            case GameManager.State.PAUSED:
                upgradeMenu.SetActive(true);
                break;
        }
    }
}
