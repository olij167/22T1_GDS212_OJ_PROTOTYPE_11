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
        ducklingStats.replenishEnergy = false;
        ducklingStats.replenishHunger = false;
        ducklingStats.replenishInterest = true;
        ducklingStats.replenishAffection = true;

        destinationSetter.enabled = true;

        //followOffset = new Vector3(player.transform.position.x - followDistance, transform.position.y, player.transform.position.x - followDistance);
        //followPosition.position = followOffset;

        destinationSetter.target = followPosition;
    }
    public void Eat()
    {
        actionTarget = destinationSetter.target;
        destinationSetter.enabled = false;

        Vector3 objectScale = actionTarget.localScale;

        actionTarget.transform.localScale = new Vector3(objectScale.x * foodSizeDecrease, objectScale.y * foodSizeDecrease, objectScale.z * foodSizeDecrease) * Time.deltaTime;

        ducklingStats.replenishHunger = true;
        ducklingStats.replenishEnergy = false;
        ducklingStats.replenishInterest = false;
        ducklingStats.replenishAffection = false;

        if (objectScale.x < foodFinishedSize)
        {
            ducklingStats.hunger += 20f;
            Destroy(actionTarget.gameObject);
        }
    }

    public void Poo()
    {

    }

    public void BePet()
    {
        ducklingStats.replenishAffection = true;

    }

    public void FindSomething()
    {
        ducklingStats.replenishEnergy = false;
        ducklingStats.replenishHunger = false;
        ducklingStats.replenishInterest = false;
        ducklingStats.replenishAffection = false;



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

        ducklingStats.replenishEnergy = false;
        ducklingStats.replenishHunger = false;
        ducklingStats.replenishInterest = true;
        ducklingStats.replenishAffection = true;



    }

    public void Sleep()
    {
        destinationSetter.enabled = false;

        ducklingStats.replenishEnergy = true;
        ducklingStats.replenishHunger = false;
        ducklingStats.replenishInterest = false;
        ducklingStats.replenishAffection = false;
    }
}
