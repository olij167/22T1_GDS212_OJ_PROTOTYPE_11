using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pathfinding;

public class ObjectSelection : MonoBehaviour
{
    public float reachDistance = 10f;

    public Transform handPos, carriedObject;
    public bool carryingObject;


    public GameObject duckling, inputPanel;
    public TextMeshProUGUI inputText;

    private DucklingBrain ducklingBrain;
    private DucklingStats ducklingStats;
    private DucklingActions ducklingActions;
    private AIDestinationSetter destinationSetter;

    public float affectionBoost, hungerBoost, interestBoost;

    private float foodSizeDecrease, foodFinishedSize;

    void Start()
    {
        inputPanel.SetActive(false);

        ducklingActions = duckling.gameObject.GetComponent<DucklingActions>();
        ducklingBrain = duckling.gameObject.GetComponent<DucklingBrain>();
        ducklingStats = ducklingBrain.ducklingStats;

        foodSizeDecrease = ducklingActions.foodSizeDecrease;
        foodFinishedSize = ducklingActions.foodFinishedSize;
    }

    // Update is called once per frame
    void Update()
    {

        if (carryingObject)
        {
            inputPanel.SetActive(true);
            inputText.text = "Right Click to Drop";

            if (Input.GetMouseButtonDown(1))
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


        if (Physics.Raycast(ray, out hit, reachDistance, 3))
        {
            if (!inputPanel.activeSelf)
            {
                inputPanel.SetActive(true);
            }

            if (!carryingObject)
            {
                if (hit.transform.CompareTag("Food") || hit.transform.CompareTag("InterestingObject") || hit.transform.CompareTag("Poo"))
                {
                    inputText.text = "Left Click to Pick Up";

                    if (Input.GetMouseButtonDown(0))
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

                    if (Input.GetMouseButtonDown(0))
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

                    if (Input.GetMouseButtonDown(0))
                    {
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

                    if (Input.GetMouseButtonDown(0))
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

                    if (Input.GetMouseButton(0))
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
            inputPanel.SetActive(false);
        }

        
    }

        
}
