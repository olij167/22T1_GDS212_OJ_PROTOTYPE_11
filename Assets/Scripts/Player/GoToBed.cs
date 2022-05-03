using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;


public class GoToBed : MonoBehaviour
{
    //fade to black, progress time to 6 am

    public Image blackScreen;

    public TimeController timeController;

    public bool isPastBedtime, isAsleep, hasSlept;
    public float asleepTimeIncrease;
    float timeSpeed;

    public TextMeshProUGUI lateText;

    public FirstPersonController playerController;

    public AudioSource audioSource;
    public AudioClip alarmAudio;

    private ObjectSelection objectSelection;

    void Start()
    {
        objectSelection = playerController.gameObject.GetComponent<ObjectSelection>();

        blackScreen.enabled = false;
        timeSpeed = timeController.timeSpeed;
        isAsleep = false;
        isPastBedtime = false;
        hasSlept = true;
    }

    
    void Update()
    {
        if ((!isAsleep && timeController.isPM && timeController.timeHours > 9f) || (!isAsleep && timeController.isPM && timeController.timeHours == 9 && timeController.timeMinutes > 30f))
        {
            lateText.enabled = true;
            isPastBedtime = true;
            lateText.text = "It's past your bedtime!";
        }

        if (!isAsleep && !hasSlept && !timeController.isPM && timeController.timeHours >= 2 && timeController.timeHours <= 6)
        {
            lateText.enabled = true;
            isPastBedtime = true;
            lateText.text = "You passed out from exhaustion";
            GoToSleep();
        }

        if (hasSlept && timeController.isPM && timeController.timeHours == 1)
        {
            hasSlept = false;
        }

        if (!isPastBedtime && isAsleep)
        {
            if (timeController.timeHours == 6 && timeController.timeMinutes >= 30f && !timeController.isPM)
            {
                WakeUp();
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(alarmAudio, audioSource.volume);
                }
            }
        }
        else if (isPastBedtime && isAsleep)
        {
            if (timeController.timeHours == 6 && timeController.timeMinutes >= 30f && !timeController.isPM)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(alarmAudio, audioSource.volume);
                }
            }

            if (timeController.timeHours == 8 && timeController.timeMinutes >= 30f && !timeController.isPM)
            {
                lateText.text = "You slept in and are Late for work! get to bed earlier next time";
                WakeUp();
            }
        }
    }

    public void GoToSleep()
    {
        if (lateText.text == "It's past your bedtime!")
        {
            lateText.text = "";
        }
        blackScreen.enabled = true;
        playerController.enabled = false;
        objectSelection.enabled = false;
        isAsleep = true;

        timeController.timeSpeed *= asleepTimeIncrease;

        
        
    }

    public void WakeUp()
    {

        blackScreen.enabled = false;
        timeController.timeSpeed = timeSpeed;
        playerController.enabled = true;
        Debug.Log("Time controller timespeed is: " + timeController.timeSpeed);
        Debug.Log("Player controller is: [" + playerController.isActiveAndEnabled + "]");
        objectSelection.enabled = true;
        isPastBedtime = false;
        isAsleep = false;
        hasSlept = true;
    }
}
