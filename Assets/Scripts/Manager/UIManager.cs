using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Slider bossHealthBar;
    public static UIManager instance;
    public GameObject pauseMenu;
    public GameObject healthBar;
    public GameObject gameOverPanel;
    public Text Score;
    public int currentScore;
    public void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void UpdateHealth(float currentHealth)
    {
        switch(currentHealth)
        {
            case 3:
                healthBar.transform.GetChild(0).gameObject.SetActive(true);
                healthBar.transform.GetChild(1).gameObject.SetActive(true);
                healthBar.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 2:
                healthBar.transform.GetChild(0).gameObject.SetActive(true);
                healthBar.transform.GetChild(1).gameObject.SetActive(true);
                healthBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 1:
                healthBar.transform.GetChild(0).gameObject.SetActive(true);
                healthBar.transform.GetChild(1).gameObject.SetActive(false);
                healthBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 0:
                healthBar.transform.GetChild(0).gameObject.SetActive(false);
                healthBar.transform.GetChild(1).gameObject.SetActive(false);
                healthBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
        }
    }

    public void PauseGame()
    {
      
        pauseMenu.SetActive(true);
        Time.timeScale = 0;//时停！咋瓦鲁多！
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;//时间开始流动
    }

    public void SetBossHealth(float maxHealth)
    {
        bossHealthBar.maxValue = maxHealth;
        bossHealthBar.value = maxHealth;
    }
    public void UpdateBossHealth(float currentHealth)
    {
        bossHealthBar.value = currentHealth;
    }
    public void GameOverUI(bool gameOver)
    {
        gameOverPanel.SetActive(gameOver);
    }
    public void SetScore()
    {
        currentScore = 0;
        Score.text = currentScore.ToString();
    }
    public void AddScore(int socre)
    {
        currentScore += socre;
        Score.text = currentScore.ToString();
    }
}
