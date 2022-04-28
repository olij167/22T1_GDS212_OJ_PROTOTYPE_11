using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TutorialUIController : MonoBehaviour
{
    public Image tutorialIcon;
    public TextMeshProUGUI tutorialInstructionsText;
    
    public List<TutorialUI> tutorialUIList;
    public int count = -1;

    public bool tutorialComplete, inputPerformed;

    //Vector3 lastMousePos = Vector3.zero;

    public AudioSource voiceOverSource;

    private void Start()
    {
        //lastMousePos = Input.mousePosition;
        //voiceOverSource = GetComponent<AudioSource>();

        //voiceOverSource.clip = tutorialUIList[0].voiceOver;
        //voiceOverSource.Play();
    }

    private void Update()
    {
        if (!voiceOverSource.isPlaying && !tutorialComplete)
        {
            count++;
            ProgressTutorial();
        }

        if (tutorialComplete)
        {
            foreach (TutorialUI tutorialUI in tutorialUIList)
            {
                tutorialUI.tutorialStepComplete = false;
                tutorialInstructionsText.enabled = false;

            }

            enabled = false;
            //gameObject.SetActive(false);
            
        }
    }

    void ProgressTutorial()
    {
        int i = count;

        if (i + 1 >= tutorialUIList.Count)
        {
            tutorialComplete = true;
        }

        tutorialIcon.sprite = tutorialUIList[i].tutorialIcon;
        tutorialInstructionsText.text = tutorialUIList[i].tutorialInstructions;

        //voiceOverSource.clip = tutorialUIList[i].voiceOver;
        voiceOverSource.PlayOneShot(tutorialUIList[i].voiceOver);

        

        //if (tutorialUIList[i + 1] != null)
        //{
        //    i++;
        //}

        if (!voiceOverSource.isPlaying)
        {
            tutorialUIList[i].tutorialStepComplete = true;
            //i++;
        }

    }




    //void Update()
    //{
    //    //if (!tutorialComplete)
    //    //{
        


    //    //if (!tutorialUIList[i].isLookAroundTutorial)
    //    //{
    //    //foreach (KeyCode tutorialInput in tutorialUIList[i].tutorialInputList)
    //    //{
    //    //    if (Input.GetKeyDown(tutorialInput))
    //    //    {
    //    //        inputPerformed = true;

    //    //    }
    //    //}

    //    //if (inputPerformed)
    //    //{
    //    //    if (!voiceOverSource.isPlaying)
    //    //    {
    //    //        tutorialUIList[i].tutorialStepComplete = true;

    //    //        if (tutorialUIList[count + 1] != null)
    //    //        {
    //    //            count++;
    //    //        }
    //    //        else tutorialComplete = true;
    //    //    }
    //    //    inputPerformed = false;
    //    //}
    //    //}

    //    //if (tutorialUIList[i].isLookAroundTutorial)
    //    //{
    //    //    voiceOverSource.clip = tutorialUIList[i].voiceOver;
    //    //    voiceOverSource.Play();

    //    //    Vector3 mouseDelta = Input.mousePosition - lastMousePos;

    //    //    if (mouseDelta.x != 0)
    //    //    {
    //    //        inputPerformed = true;


    //    //    }


    //    //    if (inputPerformed)
    //    //    {
    //    //        if (!voiceOverSource.isPlaying)
    //    //        {
    //    //            tutorialUIList[i].tutorialStepComplete = true;
    //    //            if (tutorialUIList[count + 1] != null)
    //    //            {
    //    //                count++;
    //    //            }
    //    //            else tutorialComplete = true;
    //    //        }
    //    //        inputPerformed = false;
    //    //    }
    //    //}
    //    //}



    //}
}
