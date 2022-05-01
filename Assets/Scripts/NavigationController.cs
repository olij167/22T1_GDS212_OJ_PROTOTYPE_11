using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NavigationController : MonoBehaviour
{
    //public UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;

    public Button startButton, quitButton, continueButton;

    public GameObject startUI, newGameUI;

    public string gameScene, menuScene;

    public TMP_InputField duckNameInput;

    public DucklingStats ducklingStats;
    //public ObjectPositions objectPositions;

    //public TutorialUIController tutorialUIController;

    public TimeSO time;

    private void Start()
    {
        //player.enabled = false;
        //tutorialUIController.enabled = false;

        Time.timeScale = 0f;
    }

    public void Continue()
    {
        SaveData data = SaveSystem.LoadData();

        ducklingStats.affection = data.affection;
        ducklingStats.energy = data.energy;
        ducklingStats.hunger = data.hunger;
        ducklingStats.interest = data.interest;

        time.timeHours = data.currentHours;
        time.timeMinutes = data.currentMinutes;
        time.timeSeconds = data.currentSeconds;
        time.currentDay = data.currentDay;

        time.isLoaded = true;

        SceneManager.LoadScene(gameScene);

    }
    public void NewGame()
    {
        newGameUI.SetActive(true);
        startUI.SetActive(false);
    }

    public void CancelNewGame()
    {
        newGameUI.SetActive(false);
        startUI.SetActive(true);
    }

    public void StartNewGame()
    {
        ducklingStats.ducklingName = duckNameInput.text;
        ducklingStats.affection = 50f;
        ducklingStats.energy = 50f;
        ducklingStats.hunger = 50f;
        ducklingStats.interest = 50f;

        time.isLoaded = false;

        SceneManager.LoadScene(gameScene);

        
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

    public void Pause()
    {
        float timeScale = Time.timeScale;
        if (Time.timeScale < 0)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = timeScale;
        }
    }
}
