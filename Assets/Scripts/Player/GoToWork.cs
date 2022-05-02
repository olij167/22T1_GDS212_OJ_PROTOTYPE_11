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
    bool atWork, beenToWork;
    string currentDay;

    public RigidbodyFirstPersonController playerController;
    public Transform atWorkTransform, frontDoorTransform;
    private ObjectSelection objectSelection;

    public GameObject getHomePanel, inputPanel;
    public TextMeshProUGUI incomeText, attendanceText, inputText, lateText;
    public bool wasEarly, wasLate;

    public DucklingStats stats;


    void Start()
    {
        timeSpeed = timeController.timeSpeed;
        duckCam.enabled = false;

        objectSelection = playerController.gameObject.GetComponent<ObjectSelection>();
        duckCamControls.enabled = false;

        getHomePanel.SetActive(false);
        lateText.text = "";
    }

    void Update()
    {
        if (!atWork && !beenToWork && !timeController.isPM && timeController.timeHours >= 8 || !atWork && !beenToWork && timeController.isPM)
        {
            lateText.text = "You're late for work!! Next time leave before 8 AM";
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

            playerController.enabled = false;
            playerController.gameObject.transform.position = atWorkTransform.position;
            timeController.timeSpeed *= atWorkTimeIncrease;

            inputText.text = "Mike is at Work";
            lateText.text = "";
            atWork = true;

        }
    }

    public void GetHome()
    {
        if (atWork)
        {
            playerController.gameObject.transform.position = frontDoorTransform.position;
            mainCam.enabled = true;
            duckCamControls.enabled = false;
            duckCam.enabled = false;

            objectSelection.enabled = true;
            timeController.timeSpeed = timeSpeed;

            stats.playerMoney += incomeForDay;
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

            if (Input.GetButton("Jump"))
            {
                wasEarly = false;
                wasLate = false;
                Time.timeScale = 1f;
                getHomePanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                playerController.enabled = true;
                beenToWork = true;
                currentDay = timeController.currentDay;
                atWork = false;
            }
        }


    }
}
