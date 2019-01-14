using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerForScenes : MonoBehaviour {

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

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel(int level)
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
