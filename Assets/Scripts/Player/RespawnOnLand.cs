using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RespawnOnLand : MonoBehaviour
{
    public Vector3 lastGroundedPosition;
    //public UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerController;
    CharacterController characterController;

    public bool inWater;

    public TextMeshProUGUI respawnText;

    public TutorialUIController tutorialUI;

    public int timesRespawned;

    void Start()
    {
        lastGroundedPosition = new Vector3();
        characterController = GetComponent<CharacterController>();
        respawnText.enabled = false;
        //GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (characterController.isGrounded)
        {
            lastGroundedPosition = transform.position;
        }

        if (inWater)
        {
            characterController.enabled = false;

            if (!tutorialUI.tutorialComplete)
            {
                tutorialUI.tutorialInstructionsText.enabled = false;
            }

            respawnText.enabled = true;



            if (Input.GetKeyDown(KeyCode.R))
            {
                TeleportToLastPos();
                inWater = false;
            }
        }

    }

    public void TeleportToLastPos()
    {
        transform.position = lastGroundedPosition + Vector3.up;
        timesRespawned++;
        respawnText.enabled = false;
        characterController.enabled = true;

        if (!tutorialUI.tutorialComplete)
        {
            tutorialUI.tutorialInstructionsText.enabled = true;
        }

    }
}
