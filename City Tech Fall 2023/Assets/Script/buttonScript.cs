using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonScript : MonoBehaviour
{
    public GameObject pauseUI;
    public bool isPaused = false;
    public void MenuScene()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
    public void GameScene()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        isPaused = true;
    }
    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
    //SceneManager.LoadScene(2) for the death scene is in the PlayerMechanics.cs script
}
