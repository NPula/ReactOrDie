using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GameObject upgradeMenu;
    public Text waveText;
    public Text scrapsText;
    public GameObject ScrapsUI;

    private void Start()
    {
        upgradeMenu.SetActive(false);
        UpdateWaveNumber();
    }

    private void Update()
    {
        switch (GameManager.Instance.state)
        {
            case GameManager.State.RUNNING:
                upgradeMenu.SetActive(false);
                ScrapsUI.SetActive(false);
                break;
            case GameManager.State.PAUSED:
                upgradeMenu.SetActive(true);
                ScrapsUI.SetActive(true);
                UpdateScrapsNumber();
                UpdateWaveNumber();
                break;
        }
    }

    public void UpdateWaveNumber()
    {
        waveText.text = GameManager.Instance.waveManager.waveNumber.ToString();
    }

    public void UpdateScrapsNumber()
    {
        ScrapsUI.transform.GetChild(1).GetComponent<Text>().text = GameManager.Instance.player.stats.scraps.ToString();
    }
}
