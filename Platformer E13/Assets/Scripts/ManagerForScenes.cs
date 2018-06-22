using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerForScenes : MonoBehaviour {

    public GameObject nextScreen, gameOver;

    private static ManagerForScenes instance;

    public static ManagerForScenes Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ManagerForScenes>();
            }
            return instance;
        }
    }

    void Start()
    {
        nextScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
    }

    public void NextLevelScreen()
    {
        Time.timeScale = 0;
        nextScreen.SetActive(true);
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
