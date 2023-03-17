using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private Door doorOpen;//获取门打开的的脚本
    public static GameManager instance;//实现单例
    public bool gameOver;

    public List<Enemy> enemies = new List<Enemy>();
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
        //player = FindObjectOfType<PlayerController>();//获取对应的脚本
        //doorOpen = FindObjectOfType<Door>();//获取对应的脚本
    }
    public void IsPlayer(PlayerController controller)
    {
        player = controller;
    }
    public void IsExitDoor(Door door)
    {
        doorOpen = door;
    }

    public void Update()
    {
        if (player != null)
        {
            gameOver = player.isDead;
        }

        UIManager.instance.GameOverUI(gameOver);
    }
    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }
    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        if(enemies.Count==0)
        {
            doorOpen.OpenDoor();
            SaveData();
        }
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      //  PlayerPrefs.DeleteKey("playerHealth");//重新加载
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }
    public void ContinueGame()
    {
        if(PlayerPrefs.HasKey("sceneIndex"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("sceneIndex"));
        }
        else
        {
            NewGame();
        }
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//通关去往下一个场景
    }
    public void QuitGame()
    {
        Application.Quit();//生成出来的游戏才行
    }

    public float LoadHealth()
    {
        if(!PlayerPrefs.HasKey("playerHealth"))
        {
            PlayerPrefs.SetFloat("playerHealth", 3f);
        }
        float currentHealth = PlayerPrefs.GetFloat("playerHealth");
        
        return currentHealth;
    }
    public void SaveData()
    {
        PlayerPrefs.SetFloat("playerHealth", player.health);
        PlayerPrefs.SetInt("sceneIndex", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.Save();
    }
}
