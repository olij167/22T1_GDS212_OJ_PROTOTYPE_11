using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;



public class TutorialUIController : MonoBehaviour
{
    //public Image tutorialIcon;
    public TextMeshProUGUI tutorialInstructionsText, inputText;
    
    public List<TutorialUI> tutorialUIList;
    public int count = -1;

    public bool tutorialComplete, inputPerformed;

    //Vector3 lastMousePos = Vector3.zero;

    //public AudioSource voiceOverSource;
    public float tutorialSlideTimer;

    public FirstPersonController playerController;

    [HideInInspector] public ObjectSelection objectSelection;

    public GameObject inputPanel, welcomeHomePanel;

    public KeyCode skipTutorial;

    private void Start()
    {
        objectSelection = playerController.transform.GetComponent<ObjectSelection>();
        playerController.transform.GetComponent<GoToWork>().enabled = false;

        //playerController.enabled = false;

        objectSelection.enabled = false;
        Time.timeScale = 0f;

        inputPanel.SetActive(true);
        welcomeHomePanel.SetActive(false);
        //lastMousePos = Input.mousePosition;
        //voiceOverSource = GetComponent<AudioSource>();

        //voiceOverSource.clip = tutorialUIList[0].voiceOver;
        //voiceOverSource.Play();
    }

    private void Update()
    {
        tutorialSlideTimer -= Time.deltaTime;
        for (int i = 0; i < tutorialUIList.Count; i++)
        {
            foreach (KeyCode tutorialInput in tutorialUIList[i].tutorialInputList)
            {
                if (Input.GetKeyDown(tutorialInput))
                {
                    inputPerformed = true;

                }
            }
        }

        if ((tutorialSlideTimer <= 0f && !tutorialComplete) || (inputPerformed && !tutorialComplete))
        {
            count++;
            inputPerformed = false;

            ProgressTutorial();
        }

        if (tutorialComplete)
        {
            foreach (TutorialUI tutorialUI in tutorialUIList)
            {
                tutorialUI.tutorialStepComplete = false;
                tutorialInstructionsText.enabled = false;

            }
            playerController.enabled = true;
            objectSelection.enabled = true;
            Time.timeScale = 1f;
            playerController.transform.GetComponent<GoToWork>().enabled = true;


            enabled = false;
            //gameObject.SetActive(false);

        }
        else inputPanel.SetActive(true);

        if (!tutorialComplete && Input.GetKeyDown(skipTutorial))
        {
            tutorialComplete = true;
        }
    }

    void ProgressTutorial()
    {
        //int i = count;

        if (count + 1 >= tutorialUIList.Count)
        {
            tutorialComplete = true;
        }

        //tutorialIcon.sprite = tutorialUIList[i].tutorialIcon;
        tutorialInstructionsText.text = tutorialUIList[count].tutorialInstructions;
        inputPanel.SetActive(true);
        inputText.enabled = true;
        inputText.text = "(Press " + tutorialUIList[count].inputString + " to continue," + "\n" + "Press " + skipTutorial.ToString() + " to skip tutorial)";

        //voiceOverSource.clip = tutorialUIList[i].voiceOver;
        //voiceOverSource.PlayOneShot(tutorialUIList[i].voiceOver);

        

        //if (tutorialUIList[i + 1] != null)
        //{
        //    i++;
        //}

        if (tutorialSlideTimer <= 0f || inputPerformed)
        {
            tutorialUIList[count].tutorialStepComplete = true;
            
            //i++;
        }

        inputPerformed = false;
        tutorialSlideTimer = tutorialUIList[count].tutorialSlideDuration;

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
