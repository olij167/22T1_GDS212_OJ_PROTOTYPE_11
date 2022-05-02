using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityStandardAssets.Characters.FirstPerson;


public class EndGame : MonoBehaviour
{
    public TimeController timeController;

    public GoToWork work;

    public CamController duckCamControls;

    public GameObject endPanel, returnDuckPanel;

    public TextMeshProUGUI returnDucklingText;

    public DucklingStats stats;

    public bool declinedReturnDecision;

    //private RigidbodyFirstPersonController playerController;

    void Start()
    {
        //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<RigidbodyFirstPersonController>();

        endPanel.SetActive(false);
        returnDuckPanel.SetActive(false);
    }

    private void Update()
    {
        if (timeController.currentDay == "Friday" && !declinedReturnDecision && work.atWork)
        {
            ReturnDuckDecision();
        }

        if (timeController.currentDay == "Saturday")
        {
            declinedReturnDecision = false;
        }
    }

    public void ReturnDuckDecision()
    {
        duckCamControls.enabled = false;
        Time.timeScale = 0f;
        returnDuckPanel.SetActive(true);

        if (!returnDucklingText.text.Contains(stats.ducklingName))
        {
            returnDucklingText.text += (stats.ducklingName + "?");
        }
    }
    public void ReturnDuck()
    {
        returnDuckPanel.SetActive(false);

        endPanel.SetActive(true);
    }

    public void DontReturnDuck()
    {
        Time.timeScale = 1f;

        declinedReturnDecision = true;

        returnDuckPanel.SetActive(false);
        duckCamControls.enabled = true;

    }
}
