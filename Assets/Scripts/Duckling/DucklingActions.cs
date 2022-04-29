using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DucklingActions : MonoBehaviour
{
    private DucklingStats ducklingStats;

    private AIDestinationSetter destinationSetter;

    //private DucklingObjectDetection objectDetection;

    public Transform player, followPosition, actionTarget;

    private Vector3 followOffset;
    public float followDistance, foodSizeDecrease, foodFinishedSize;

    public List<Transform> wanderWaypoints;

    bool waypointSet = false;



    void Start()
    {
        ducklingStats = GetComponent<DucklingStats>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        //objectDetection = GetComponent<DucklingObjectDetection>();

        wanderWaypoints = new List<Transform>();

        foreach (GameObject wayPoint in GameObject.FindGameObjectsWithTag("WanderWaypoint"))
        {
            wanderWaypoints.Add(wayPoint.transform);
        }
    }

    void Update()
    {
       
    }

    
    // Action Methods
    public void FollowPlayer()
    {
        ducklingStats.energy -= ducklingStats.energyTimerDecrease * Time.deltaTime;
        ducklingStats.hunger -= ducklingStats.hungerTimerDecrease * Time.deltaTime;

        destinationSetter.enabled = true;

        followOffset = new Vector3(player.transform.position.x - followDistance, transform.position.y, player.transform.position.x - followDistance);
        followPosition.position = followOffset;

        destinationSetter.target = followPosition;
    }
    public void Eat()
    {
        actionTarget = destinationSetter.target;
        destinationSetter.enabled = false;

        Vector3 objectScale = actionTarget.localScale;

        actionTarget.transform.localScale = new Vector3(objectScale.x * foodSizeDecrease, objectScale.y * foodSizeDecrease, objectScale.z * foodSizeDecrease) * Time.deltaTime;

        ducklingStats.hunger += ducklingStats.hungerReplenishRate * Time.deltaTime;

        if (objectScale.x < foodFinishedSize)
        {
            Destroy(actionTarget.gameObject);
        }
    }

    public void Poo()
    {

    }

    public void BePet()
    {
        ducklingStats.affection += ducklingStats.affectionReplenishRate * Time.deltaTime;

    }

    public void FindSomething()
    {
        ducklingStats.energy -= ducklingStats.energyTimerDecrease * Time.deltaTime;
        ducklingStats.hunger -= ducklingStats.hungerTimerDecrease * Time.deltaTime;
        ducklingStats.interest -= ducklingStats.interestTimerDecrease * Time.deltaTime;
        ducklingStats.affection -= ducklingStats.affectionTimerDecrease * Time.deltaTime;



        destinationSetter.enabled = true;

        if (!waypointSet)
        {
            destinationSetter.target = wanderWaypoints[Random.Range(0, wanderWaypoints.Count)];
            waypointSet = true;
        }

        if (Vector3.Distance(transform.position, destinationSetter.target.position) < 1f)
        {
            waypointSet = false;
        }
    }

    public void PlayWithObject()
    {
        actionTarget = destinationSetter.target;
        destinationSetter.enabled = false;

        ducklingStats.energy -= ducklingStats.energyTimerDecrease * Time.deltaTime;
        ducklingStats.hunger -= ducklingStats.hungerTimerDecrease * Time.deltaTime;
        ducklingStats.interest += ducklingStats.interestReplenishRate * Time.deltaTime;
        ducklingStats.affection += ducklingStats.affectionReplenishRate * Time.deltaTime;



    }

    public void Sleep()
    {
        destinationSetter.enabled = false;

        ducklingStats.energy += ducklingStats.energyReplenishRate * Time.deltaTime;
    }
}
