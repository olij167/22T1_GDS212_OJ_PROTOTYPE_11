using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DucklingBrain : MonoBehaviour
{
    private DucklingStats ducklingStats;
    private DucklingActions ducklingActions;
    private DucklingObjectDetection objectDetection;
    private AIDestinationSetter destinationSetter;


    public Transform ducklingTarget;

    public float stateTimer, maxStateTimer = 90f, minStateTimer = 20f, lowestStat;
    public int pooOddsMaximum;

    public string currentState, lowestStatName;

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
                    ResetStateTimer();
                    AttentionSeekingState();
                    break;
                }
            
            case "energy":
                {
                    ResetStateTimer();
                    TiredState();
                    break;
                }
            
            case "hunger":
                {
                    ResetStateTimer();
                    HungryState();
                    break;
                }
            
            case "interest":
                {
                    ResetStateTimer();
                    FindActivityState();
                    break;
                }
            case "N/A":
                {
                    ResetStateTimer();
                    FindActivityState();
                    break;
                }
        }
    }

    public void ResetStateTimer()
    {
        stateTimer = Random.Range(minStateTimer, maxStateTimer);

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

        for (int i = 0; i < objectDetection.detectedObjects.Count; i++)
        {
            if (!objectDetection.detectedObjects[i].CompareTag("Food"))
            {
                ducklingActions.FindSomething();
            }
            else
            {
                foodFound = true;
                destinationSetter.target = objectDetection.detectedObjects[i];
            }
        }

        if (foodFound && Vector3.Distance(transform.position, destinationSetter.target.position) <= 1f)
        {
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
        currentState = "Tired";


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
    }

    public void FindActivityState()
    {
        currentState = "Wandering Around";
        bool activityFound = false;

        //ducklingStats.energy -= ducklingStats.energyTimerDecrease * Time.deltaTime;

        ducklingActions.FindSomething();

        for (int i = 0; i < objectDetection.detectedObjects.Count; i++)
        {
            if (!objectDetection.detectedObjects[i].CompareTag("InterestingObject"))
            {
                ducklingActions.FindSomething();
            }
            else
            {
                activityFound = true;
                destinationSetter.target = objectDetection.detectedObjects[i];
            }
        }

        if (activityFound && Vector3.Distance(transform.position, destinationSetter.target.position) <= 1f)
        {
            ducklingActions.PlayWithObject();
        }
    }
}
