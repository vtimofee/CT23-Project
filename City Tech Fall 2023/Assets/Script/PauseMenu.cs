using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pauseMenu;
    public static bool isPaused = false;

    public TextMeshProUGUI randomTextField;
    void Start()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(isPaused == true)
            {
                ResumeGame();
            }
            else if (isPaused == false)
            {
                PauseGame();
            }
        }
        
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        randomTextField.text = RandomText();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public string RandomText()
    {
        string[] randomFax = {"Hello","Whaddup","GOOD MORNIIIIING" };
        string randomFact = randomFax[Random.Range(0, randomFax.Length)];
        return randomFact;

    }

}


