using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Pathfinding;

public class DucklingBrain : MonoBehaviour
{
    [HideInInspector] public DucklingStats ducklingStats;
    [HideInInspector] public DucklingActions ducklingActions;
    [HideInInspector] public DucklingObjectDetection objectDetection;
    [HideInInspector] public AIDestinationSetter destinationSetter;


    public Transform ducklingHead;

    public float stateTimer, lowestStat, stateTimerReset; //maxStateTimer = 90f, minStateTimer = 20f;
    public int pooOddsMaximum;

    public string currentState, lowestStatName;
    public Color stateColour = Color.grey;

    public bool hasEaten;



    void Start()
    {
        ducklingStats = GetComponent<DucklingStats>(); 
        ducklingActions = GetComponent<DucklingActions>();
        objectDetection = GetComponent<DucklingObjectDetection>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        ResetStateTimer();
        ChooseState();
    }

    void Update()
    {
        //FindActivityState();
        
        if (currentState != "Choosing Next State")
        stateTimer -= Time.deltaTime;

        if (stateTimer <= 0 || currentState == null)
        {
            ChooseState();
        }
    }

    public void ChooseState()
    {
        currentState = "Choosing Next State";

        GetLowestStat();

        if (hasEaten)
        {
            int pooChance = Random.Range(0, pooOddsMaximum);

            if (pooChance == 0)
            {
                ducklingActions.Poo();
            }
        }
       
        switch (lowestStatName)
        {
            case "affection":
                {
                    stateColour = Color.magenta;
                    ResetStateTimer();
                    AttentionSeekingState();
                    break;
                }
            
            case "energy":
                {
                    stateColour = Color.yellow;
                    ResetStateTimer();
                    TiredState();
                    break;
                }
            
            case "hunger":
                {
                    stateColour = Color.blue;
                    ResetStateTimer();
                    HungryState();
                    break;
                }
            
            case "interest":
                {
                    stateColour = Color.cyan;
                    ResetStateTimer();
                    FindActivityState();
                    break;
                }
            case "N/A":
                {
                    stateColour = Color.grey;
                    ResetStateTimer();
                    FindActivityState();
                    break;
                }
        }
    }

    public void ResetStateTimer()
    {
        //stateTimer = Random.Range(minStateTimer, maxStateTimer);
        stateTimer = stateTimerReset;

    }

    public string GetLowestStat()
    {
        lowestStat = Mathf.Min(ducklingStats.affection, ducklingStats.energy, ducklingStats.hunger, ducklingStats.interest);

        if (lowestStat == ducklingStats.affection)
        {
            return lowestStatName = "affection";
        }
        else if (lowestStat == ducklingStats.energy)
        {
            return lowestStatName = "energy";
        }
        else if (lowestStat == ducklingStats.hunger)
        {
            return lowestStatName = "hunger";
        }
        else if (lowestStat == ducklingStats.interest)
        {
            return lowestStatName = "interest";
        }
        else return lowestStatName = "N/A";
    }

    public void HungryState()
    {
        bool foodFound = false;

        currentState = "Hungry";

        objectDetection.objectDetectionActivated = true;

        if (!foodFound)
        {
            ducklingActions.FindSomething();
        }

        for (int i = 0; i < objectDetection.detectedObjects.Count; i++)
        {
            if (objectDetection.detectedObjects[i].CompareTag("Food"))
            {
                foodFound = true;

                destinationSetter.target = objectDetection.detectedObjects[i];

                objectDetection.objectDetectionActivated = false;
            }
        }

        if (foodFound && Vector3.Distance(transform.position, destinationSetter.target.position) <= 1f)
        {
            currentState = "Eating";

            stateColour = Color.red;
            
            ducklingActions.Eat();
            hasEaten = true;
        }

        if (ducklingStats.hunger >= 100f)
        {
            ChooseState();
        }
    }

    public void TiredState()
    {
        currentState = "Sleep";


        ducklingActions.Sleep();

        if (ducklingStats.energy >= 100f || stateTimer <= 0f)
        {
            ResetStateTimer();
            ChooseState();
        }
    }

    public void AttentionSeekingState()
    {
        currentState = "Attention Seeking";

        ducklingActions.FollowPlayer();

        if (Vector3.Distance(transform.position, ducklingActions.player.position) < ducklingStats.lookRadius)
        {
            currentState = "Playing With You";
            stateColour = Color.green;
            ducklingActions.PlayWithPlayer();
        }
    }

    public void FindActivityState()
    {
        currentState = "Find Activity";
        bool activityFound = false;

        objectDetection.objectDetectionActivated = true;

        //ducklingStats.energy -= ducklingStats.energyTimerDecrease * Time.deltaTime;

        ducklingActions.FindSomething();

        for (int i = 0; i < objectDetection.detectedObjects.Count; i++)
        {
            if (objectDetection.detectedObjects[i].CompareTag("InterestingObject") || objectDetection.detectedObjects[i].CompareTag("Player"))
            {
                activityFound = true;
                destinationSetter.target = objectDetection.detectedObjects[i];
                objectDetection.objectDetectionActivated = false;
            }
            //else
            //{
            //    ducklingActions.FindSomething();
            //}
        }

        if (activityFound && destinationSetter.target.CompareTag("InterestingObject") && Vector3.Distance(transform.position, destinationSetter.target.position) <= .5f)
        {
            currentState = "Play with Toy";
            stateColour = Color.green;
            ducklingActions.PlayWithObject();
        }

        if (activityFound && destinationSetter.target.CompareTag("Player") && Vector3.Distance(transform.position, destinationSetter.target.position) <= .5f)
        {
            currentState = "Play with You";
            stateColour = Color.green;
            ducklingActions.PlayWithPlayer();
        }
    }

    public void PlayState()
    {
        currentState = "Play with Toy From You";
        stateColour = Color.green;
        ducklingActions.PlayWithObject();
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = stateColour;
            Gizmos.DrawWireSphere(ducklingHead.position, ducklingStats.lookRadius);

            Handles.Label(transform.position, currentState);
        }
    }
}
