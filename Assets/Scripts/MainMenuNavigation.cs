using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuNavigation : MonoBehaviour
{
    //public UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;

    //public Button startButton, quitButton, continueButton;

    public GameObject startUI, newGameUI;

    public string gameScene, menuScene;

    public TMP_InputField duckNameInput;

    public DucklingStats ducklingStats;
    //public ObjectPositions objectPositions;

    //public TutorialUIController tutorialUIController;

    public TimeSO time;

    //public ParticleSystem duckParticles;

    private void Start()
    {
        //player.enabled = false;
        //tutorialUIController.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;

        Time.timeScale = 1f;

        //duckParticles.Play();
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

        //List<Vector3> toyPositionList = new List<Vector3>();
        //List<Vector3> foodPositionList = new List<Vector3>();

        //foreach (float[] foodPosition in data.foodPositionList)
        //{
        //    Vector3 position;
        //    position.x = foodPosition[0];
        //    position.y = foodPosition[1];
        //    position.z = foodPosition[2];
        //    objectPositions.foodPositions.Add(position);
        //}
        
        //foreach (float[] toyPosition in data.toyPositionList)
        //{
        //    Vector3 position;
        //    position.x = toyPosition[0];
        //    position.y = toyPosition[1];
        //    position.z = toyPosition[2];
        //    objectPositions.toyPositions.Add(position);
        //}

        //objectPositions.isLoaded = true;

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
        ducklingStats.playerMoney = 8f;

        time.isLoaded = false;
        //objectPositions.isLoaded = false;

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

    
}
