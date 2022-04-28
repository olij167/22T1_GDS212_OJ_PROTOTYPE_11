using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavigationController : MonoBehaviour
{
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;

    public Button startButton, quitButton, restartButton;

    public GameObject startUI;

    public string sceneName;

    public TutorialUIController tutorialUIController;

    private void Start()
    {
        player.enabled = false;
        tutorialUIController.enabled = false;

        Time.timeScale = 0f;
    }
    public void StartGame()
    {
        player.enabled = true;
        tutorialUIController.enabled = true;


        Time.timeScale = 1f;

        startUI.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
