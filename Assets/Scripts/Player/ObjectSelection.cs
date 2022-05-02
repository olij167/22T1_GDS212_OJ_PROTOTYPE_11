using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pathfinding;
using UnityStandardAssets.Characters.FirstPerson;


public class ObjectSelection : MonoBehaviour
{
    public float reachDistance = 10f;

    public Transform handPos, carriedObject;
    public bool carryingObject;

    private FirstPersonController playerController;

    public GameObject duckling, inputPanel, shopUI;
    public TextMeshProUGUI inputText;

    private DucklingBrain ducklingBrain;
    private DucklingStats ducklingStats;
    private DucklingActions ducklingActions;
    private AIDestinationSetter destinationSetter;

    public float affectionBoost, hungerBoost, interestBoost;

    private float foodSizeDecrease, foodFinishedSize;

    private GoToWork goToWork;
    private GoToBed goToBed;

    public TimeController timeController;

    public LayerMask layerMask;
    //int ignoreMask = 1 << 3 | 1 << 0;

    void Start()
    {
        inputPanel.SetActive(false);

        ducklingActions = duckling.gameObject.GetComponent<DucklingActions>();
        ducklingBrain = duckling.gameObject.GetComponent<DucklingBrain>();
        ducklingStats = ducklingBrain.ducklingStats;

        foodSizeDecrease = ducklingActions.foodSizeDecrease;
        foodFinishedSize = ducklingActions.foodFinishedSize;

        goToWork = GetComponent<GoToWork>();
        goToBed = GetComponent<GoToBed>();
        playerController = goToWork.playerController;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (carryingObject)
        {
            inputPanel.SetActive(true);
            inputText.text = "Right Click to Drop";

            if (Input.GetButtonDown("Fire2"))
            {
                carriedObject.GetComponent<Rigidbody>().useGravity = true;
                carriedObject.GetComponent<Rigidbody>().isKinematic = false;

                carriedObject.parent = null;
                carriedObject = null;
                carryingObject = false;
                inputPanel.SetActive(false);
            }
        }


        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        layerMask = 1 << 3 | 1 << 8;

        if (Physics.Raycast(ray, out hit, reachDistance, ~layerMask))
        {
            if (!inputPanel.activeSelf)
            {
                inputPanel.SetActive(true);
            }

            if (hit.transform.CompareTag("Computer"))
            {
                inputText.text = "Left Click to Shop Online";

                if (Input.GetButtonDown("Fire1"))
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    playerController.enabled = false;
                    shopUI.SetActive(true);
                }
            }

            if (hit.transform.CompareTag("Bed") && timeController.isPM)
            {
                inputText.text = "Left Click to Sleep";

                if (Input.GetButtonDown("Fire1"))
                {
                    goToBed.GoToSleep();
                }
            }
            
            if (hit.transform.CompareTag("FrontDoor") && !timeController.isPM)
            {
                inputText.text = "Left Click to Go to Work";

                if (Input.GetButtonDown("Fire1"))
                {
                    goToWork.AtWork();
                }
            }

            if (!carryingObject)
            {
                if (hit.transform.CompareTag("Food") || hit.transform.CompareTag("InterestingObject") || hit.transform.CompareTag("Poo"))
                {
                    inputText.text = "Left Click to Pick Up";

                    if (Input.GetButtonDown("Fire1"))
                    {
                        carriedObject = hit.transform;
                        carriedObject.GetComponent<Rigidbody>().useGravity = false;
                        carriedObject.GetComponent<Rigidbody>().isKinematic = true;

                        carriedObject.parent = handPos;
                        carriedObject.position = handPos.position;

                        carryingObject = true;
                    }
                }

                if (hit.transform.CompareTag("Duckling"))
                {
                    inputText.text = "Left Click to Pat";

                    if (Input.GetButtonDown("Fire1"))
                    {
                        Debug.Log("You pet the duck");
                        ducklingStats.affection += affectionBoost;

                        ducklingActions.particleSystem.textureSheetAnimation.SetSprite(0, ducklingActions.affectionSprite);
                        var col = ducklingActions.particleSystem.colorOverLifetime;
                        col.color = ducklingActions.affectionParticleColour;

                        if (!ducklingActions.particleSystem.isPlaying)
                        {
                            ducklingActions.particleSystem.Play();
                        }
                    }
                }
            }
            else if (hit.transform.CompareTag("Duckling"))
            {
                if (carriedObject.CompareTag("Food"))
                {
                    inputText.text = "Left Click to Feed";

                    if (Input.GetButtonDown("Fire1"))
                    {
                        ducklingBrain.hasEaten = true;
                        //Vector3 objectScale = carriedObject.localScale;
                        carriedObject.transform.localScale -= new Vector3(carriedObject.transform.localScale.x * foodSizeDecrease, carriedObject.transform.localScale.y * foodSizeDecrease, carriedObject.transform.localScale.z * foodSizeDecrease);

                        ducklingStats.hunger += hungerBoost;
                        ducklingStats.affection += affectionBoost;
                        

                        if (carriedObject.transform.localScale.y < foodFinishedSize)
                        {
                            if (ducklingBrain.gameObject.GetComponent<DucklingObjectDetection>().detectedObjects.Contains(carriedObject))
                            {
                                ducklingBrain.gameObject.GetComponent<DucklingObjectDetection>().detectedObjects.Remove(carriedObject);
                            }
                            Destroy(carriedObject.gameObject);

                            
                            carriedObject = null;
                            carryingObject = false;
                        }

                        ducklingActions.particleSystem.textureSheetAnimation.SetSprite(0, ducklingActions.affectionSprite);
                        var col = ducklingActions.particleSystem.colorOverLifetime;
                        col.color = ducklingActions.affectionParticleColour;

                        if (!ducklingActions.particleSystem.isPlaying)
                        {
                            ducklingActions.particleSystem.Play();
                        }
                    }
                }
                else if (carriedObject.CompareTag("InterestingObject"))
                {
                    inputText.text = "Left Click to Give Toy";

                    if (Input.GetButtonDown("Fire1"))
                    {
                        ducklingBrain.destinationSetter.target = carriedObject.transform;
                        ducklingBrain.PlayState();

                        ducklingStats.affection += affectionBoost;
                        ducklingStats.interest += interestBoost;

                        ducklingActions.particleSystem.textureSheetAnimation.SetSprite(0, ducklingActions.affectionSprite);
                        var col = ducklingActions.particleSystem.colorOverLifetime;
                        col.color = ducklingActions.affectionParticleColour;

                        if (!ducklingActions.particleSystem.isPlaying)
                        {
                            ducklingActions.particleSystem.Play();
                        }
                    }

                    if (ducklingBrain.currentState == "Play with Toy From You" && carryingObject)
                    {
                        carriedObject.GetComponent<Rigidbody>().useGravity = true;
                        carriedObject.GetComponent<Rigidbody>().isKinematic = false;

                        carriedObject.parent = null;
                        carriedObject = null;
                        carryingObject = false;


                    }
                }

                

            }
            else if (hit.transform.CompareTag("Bin"))
            {
                if (carriedObject.CompareTag("Poo"))
                {
                    inputText.text = "Left Click to Dispose";

                    if (Input.GetButtonDown("Fire1"))
                    {
                        Destroy(carriedObject.gameObject);

                        carryingObject = false;
                        carriedObject = null;
                    }
                }
                else inputText.text = "Bin";
            }
        }
        else if (!carryingObject)
        {
            inputText.text = null;
            inputPanel.SetActive(false);
        }

        
    }

        
}
