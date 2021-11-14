using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GameObject upgradeMenu;
    public GameObject pauseScreen;
    public GameObject gameOver;
    public Text waveText;
    public Text scrapsText;
    public GameObject scrapsUI;

    private PlayerController m_player;

    private void Start()
    {
        m_player = FindObjectOfType<PlayerController>();
        upgradeMenu.SetActive(false);
        UpdateWaveNumber();
    }

    private void Update()
    {
        switch (GameManager.Instance.state)
        {
            case GameManager.State.RUNNING:
                upgradeMenu.SetActive(false);
                scrapsUI.SetActive(false);
                pauseScreen.SetActive(false);
                gameOver.SetActive(false);
                break;
            case GameManager.State.PAUSED:
                pauseScreen.SetActive(true);
                break;
            case GameManager.State.UPGRADES:
                upgradeMenu.SetActive(true);
                scrapsUI.SetActive(true);
                UpdateScrapsNumber();
                UpdateWaveNumber();
                break;
            case GameManager.State.GAMEOVER:
                gameOver.SetActive(true);
                break;
        }
    }

    public void UpdateWaveNumber()
    {
        waveText.text = WaveManager.Instance.waveNumber.ToString();
    }

    public void UpdateScrapsNumber()
    {
        scrapsUI.transform.GetChild(1).GetComponent<Text>().text = m_player.stats.scraps.ToString();
    }
}
