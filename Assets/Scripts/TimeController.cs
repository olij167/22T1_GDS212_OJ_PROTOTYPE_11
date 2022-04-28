using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public float turnOver = 59.7f, timeSpeed = 1f;

    public int startTimeHours;

    [HideInInspector] public int timeHours;

    public float startTimeMinutes, startTimeSeconds;
    [HideInInspector] public float timeMinutes, timeSeconds;
    public bool isPM;

    public TextMeshProUGUI timeText;

    void Start()
    {
        timeHours = startTimeHours;
        timeMinutes = startTimeMinutes;
        timeSeconds = startTimeSeconds;

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
    }
}
