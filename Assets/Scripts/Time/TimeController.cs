using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{

    public string startDay;
    [HideInInspector] public string currentDay;

    public float turnOver = 59.7f, timeSpeed = 1f;

    public int startTimeHours;

    [HideInInspector] public int timeHours;

    public float startTimeMinutes, startTimeSeconds;
    [HideInInspector] public float timeMinutes, timeSeconds;
    public bool isPM;

    public TextMeshProUGUI timeText, dayText;

    public TimeSO timeSave;

    void Start()
    {
        if (timeSave.isLoaded)
        {
            timeHours = (int)timeSave.timeHours;
            timeMinutes = timeSave.timeMinutes;
            timeSeconds = timeSave.timeSeconds;

            currentDay = timeSave.currentDay;

        }
        else
        {
            timeHours = startTimeHours;
            timeMinutes = startTimeMinutes;
            timeSeconds = startTimeSeconds;

            currentDay = startDay;
        }

        dayText.text = currentDay;

    }

    void Update()
    {
        timeSeconds += Time.deltaTime * timeSpeed;

        if (timeSeconds >= turnOver)
        {
            timeMinutes++;
            timeSeconds = 0f;
        }

        if (timeMinutes >= turnOver)
        {
            timeHours++;
            timeMinutes = 0f;
        }

        if (timeHours >= 12)
        {
            timeHours = 0;

            if (isPM)
            {
                ProgressDays();
                dayText.text = currentDay;
                isPM = false;
            }
            else
            {
                isPM = true;
            }
        }

        if (isPM)
        {
            timeText.text = timeHours.ToString("00") + ":" + timeMinutes.ToString("00") + ":" + timeSeconds.ToString("00") + " pm";
        }
        else
        {
            timeText.text = timeHours.ToString("00") + ":" + timeMinutes.ToString("00") + ":" + timeSeconds.ToString("00") + " am";
        }

        SaveTime();

    }

    void ProgressDays()
    {
        switch (currentDay)
        {
            case "Sunday":
                {
                    currentDay = "Monday";
                    break;
                }

            case "Monday":
                {
                    currentDay = "Tuesday";
                    break;
                }

            case "Tuesday":
                {
                    currentDay = "Wednesday";
                    break;
                }

            case "Wednesday":
                {
                    currentDay = "Thursday";
                    break;
                }

            case "Thursday":
                {
                    currentDay = "Friday";
                    break;
                }

            case "Friday":
                {
                    currentDay = "Saturday";
                    break;
                }
            
            case "Saturday":
                {
                    currentDay = "Sunday";
                    break;
                }
        }

    }

    public void SaveTime()
    {
        timeSave.timeHours = timeHours;
        timeSave.timeMinutes = timeMinutes;
        timeSave.timeSeconds = timeSeconds;
        timeSave.currentDay = currentDay;
    }
}
