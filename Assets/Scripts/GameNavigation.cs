using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameNavigation : MonoBehaviour
{
    
    public string gameScene, menuScene;
    public TimeSO time;
    public DucklingStats ducklingStats;
    public GameObject pauseUI;
    public RigidbodyFirstPersonController playerController;

    //public Button saveButton, menuButton, quitButton;

    UnityEvent pauseGame, resumeGame;

    void Awake()
    {
        pauseUI.SetActive(false);
        playerController.enabled = true;
        
        pauseGame = new UnityEvent();
        pauseGame.AddListener(EnablePause);
        
        resumeGame = new UnityEvent();
        resumeGame.AddListener(DisablePause);

        //saveButton.onClick.AddListener(SaveGame);
        //menuButton.onClick.AddListener(MainMenu);
        //quitButton.onClick.AddListener(QuitGame);


        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseUI.activeSelf)
            {
                pauseGame.Invoke();
            }
            else
            {
                resumeGame.Invoke();

            }
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        SaveSystem.SaveData(ducklingStats, time);
    }

    public void EnablePause()
    {
        playerController.enabled = false;

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;

        pauseUI.SetActive(true);
    }

    public void DisablePause()
    {
        playerController.enabled = true;

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;

        pauseUI.SetActive(false);

    }
}
