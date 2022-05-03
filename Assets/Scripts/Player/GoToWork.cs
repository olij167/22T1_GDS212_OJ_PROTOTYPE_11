using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;


public class GoToWork : MonoBehaviour
{
    //increase time speed, set camera to follow duckling

    public float baseIncome, incomeForDay, incomeBonus;

    public Camera mainCam, duckCam;
    public CamController duckCamControls;

    public TimeController timeController;

    float timeSpeed;
    public float atWorkTimeIncrease;
    public bool atWork, beenToWork;
    string currentDay;

    public FirstPersonController playerController;
    public Transform atWorkTransform, frontDoorTransform;
    private ObjectSelection objectSelection;

    public GameObject getHomePanel, inputPanel;
    private GameObject player;
    public TextMeshProUGUI incomeText, attendanceText, inputText, lateText;
    public bool wasEarly, wasLate;

    public DucklingStats stats;

    public Canvas canvas;

    public EndGame endGame;

    public TutorialUIController tutorialController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        timeSpeed = timeController.timeSpeed;
        duckCam.enabled = false;

        objectSelection = playerController.gameObject.GetComponent<ObjectSelection>();
        duckCamControls.enabled = false;

        getHomePanel.SetActive(false);
        lateText.text = "";

        currentDay = timeController.currentDay;

        beenToWork = true;
    }

    void Update()
    {
        if (!atWork && !beenToWork && currentDay != "Tuesday" && !tutorialController.enabled)
        {
            if ((!timeController.isPM && timeController.timeHours >= 8 ||  timeController.isPM))
            {
                lateText.text = "You're late for work!! Next time leave before 8 AM";
            }
        }

        if (atWork && timeController.isPM && timeController.timeHours >= 5f && timeController.timeMinutes >= 30f)
        {
            GetHome();
        }

        if (beenToWork && currentDay != timeController.currentDay)
        {
            beenToWork = false;
        }
    }

    public void AtWork()
    {
        if (!atWork)
        {
            

            // Early For Work Bonus
            if ((!timeController.isPM && timeController.timeHours < 6) || (timeController.timeHours == 6 && timeController.timeMinutes < 30f))
            {
                incomeForDay = baseIncome + (baseIncome * incomeBonus);
                wasEarly = true;
                wasLate = false;
            }

            //Late For Work Penalty
            if ((!timeController.isPM && timeController.timeHours >= 8) || timeController.isPM)
            {
                incomeForDay = baseIncome - (baseIncome * incomeBonus);
                wasLate = true;
                wasEarly = false;
            }

            duckCam.enabled = true;
            
            objectSelection.enabled = false;
            mainCam.enabled = false;
            duckCamControls.enabled = true;
            canvas.worldCamera = duckCam;

            playerController.enabled = false;
            playerController.gameObject.GetComponent<ObjectSelection>().enabled = false;

            //playerController.gameObject.transform.position = atWorkTransform.position;
            Teleport(atWorkTransform.position);
            timeController.timeSpeed *= atWorkTimeIncrease;

            inputText.text = "Mike is at Work";
            lateText.text = "";
            beenToWork = true;
            atWork = true;

        }
    }

    public void GetHome()
    {
        if (atWork)
        {
            
            mainCam.enabled = true;
            duckCamControls.enabled = false;
            duckCam.enabled = false;

            objectSelection.enabled = true;
            timeController.timeSpeed = timeSpeed;

            canvas.worldCamera = mainCam;


            getHomePanel.SetActive(true);
            incomeText.text = "You earned $" + incomeForDay.ToString();

            if (wasEarly)
            {
                attendanceText.text = "including a bonus $" + (baseIncome * incomeBonus).ToString() + " for starting so early!";
            }
            
            if (wasLate)
            {
                attendanceText.text = "including $" + (baseIncome * incomeBonus).ToString() + " deducted for being late";
            }

            if (!wasEarly && !wasLate)
            {
                attendanceText.text = "You made it to work on time!";
            }

            Time.timeScale = 0f;
            

            inputPanel.SetActive(true);
            inputText.text = "Press Spacebar to Continue";

            if (lateText.text == "You're late for work!! Next time leave before 8am")
            {
                lateText.text = "";
            }

            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Cancel"))
            {
                wasEarly = false;
                wasLate = false;
                Time.timeScale = 1f;
                getHomePanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                playerController.enabled = true;
                //playerController.gameObject.transform.position = frontDoorTransform.position;
                Teleport(frontDoorTransform.position);

                currentDay = timeController.currentDay;
                stats.playerMoney += incomeForDay;
                playerController.gameObject.GetComponent<ObjectSelection>().enabled = true;
                atWork = false;
            }
        }

        
    }

    public void Teleport(Vector3 targetPosition)
    {
        Debug.Log("Teleported to [" + targetPosition + "]");
        player.transform.position = targetPosition;
    }
}
