using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DucklingActions : MonoBehaviour
{
    private DucklingStats ducklingStats;
    private DucklingBrain ducklingBrain;

    private AIDestinationSetter destinationSetter;

    //private DucklingObjectDetection objectDetection;

    public Transform player, followPosition, actionTarget, pooPrefab;

    //private Vector3 followOffset;
    public float followDistance, foodSizeDecrease, foodFinishedSize;

    public int minPoo, maxPoo;

    public List<Transform> wanderWaypoints;

    public bool waypointSet = false, isAsleep;

    public new ParticleSystem particleSystem;

    public Sprite affectionSprite, energySprite, interestSprite, excitementSprite, hungerSprite;
    public Gradient affectionParticleColour, energyParticleColour, interestParticleColour, excitementColour, hungerColour;

    void Awake()
    {
        ducklingBrain = GetComponent<DucklingBrain>();
        ducklingStats = ducklingBrain.ducklingStats;
        destinationSetter = GetComponent<AIDestinationSetter>();
        //objectDetection = GetComponent<DucklingObjectDetection>();

        wanderWaypoints = new List<Transform>();

        

        foreach (GameObject wayPoint in GameObject.FindGameObjectsWithTag("WanderWaypoint"))
        {
            wanderWaypoints.Add(wayPoint.transform);
        }
        SetEyesStatus();
    }

    void Update()
    {
       
    }

    public void SetEyesStatus()
    {
        if (isAsleep)
        {
            ducklingBrain.openEyes.SetActive(false);
            ducklingBrain.closedEyes.SetActive(true);
        }
        else
        {
            ducklingBrain.openEyes.SetActive(true);
            ducklingBrain.closedEyes.SetActive(false);
        }

        //if (ducklingBrain.closedEyes.activeSelf)
        //{
        //    if (!particleSystem.textureSheetAnimation.GetSprite(0) == energySprite || !particleSystem.isPlaying)
        //    {
        //        ducklingBrain.openEyes.SetActive(true);
        //        ducklingBrain.closedEyes.SetActive(false);
        //    }
        //}
    }

    
    // Action Methods
    public void FollowPlayer()
    {
        isAsleep = false;

        particleSystem.textureSheetAnimation.SetSprite(0, interestSprite);
        var col = particleSystem.colorOverLifetime;
        col.color = interestParticleColour;

        if (!particleSystem.isPlaying)
        {
            particleSystem.Play();
        }

        ducklingStats.replenishEnergy = false;
        ducklingStats.replenishHunger = false;
        ducklingStats.replenishInterest = true;
        ducklingStats.replenishAffection = false;

        destinationSetter.enabled = true;

        //followOffset = new Vector3(player.transform.position.x - followDistance, transform.position.y, player.transform.position.x - followDistance);
        //followPosition.position = followOffset;
        destinationSetter.target = followPosition;
        transform.LookAt(player);

    }
    public void Eat()
    {
        isAsleep = false;


        particleSystem.textureSheetAnimation.SetSprite(0, hungerSprite);
        var col = particleSystem.colorOverLifetime;
        col.color = hungerColour;

        if (!particleSystem.isPlaying)
        {
            particleSystem.Play();
        }

        actionTarget = destinationSetter.target;
        destinationSetter.enabled = false;

        Vector3 objectScale = actionTarget.localScale;

        actionTarget.transform.localScale -= new Vector3(objectScale.x * foodSizeDecrease, objectScale.y * foodSizeDecrease, objectScale.z * foodSizeDecrease) * Time.deltaTime;

        ducklingStats.replenishHunger = true;
        ducklingStats.replenishEnergy = false;
        ducklingStats.replenishInterest = false;
        ducklingStats.replenishAffection = false;

        if (objectScale.y < foodFinishedSize)
        {
            ducklingStats.hunger += 20f;
            destinationSetter.gameObject.GetComponent<DucklingObjectDetection>().detectedObjects.Remove(actionTarget);
            Destroy(actionTarget.gameObject);

            actionTarget = null;
        }

        if (!ducklingBrain.objectDetection.detectedObjects.Contains(actionTarget))
        {
            actionTarget = null;
        }
    }

    public void Poo()
    {
        isAsleep = false;

        int pooNum = Random.Range(minPoo, maxPoo);
        GameObject[] pooArray = new GameObject[pooNum];

        for (int i = 0; i < pooNum; i++)
        {
           pooArray[i] = Instantiate(pooPrefab.gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z - (transform.lossyScale.z / 2)), Quaternion.Euler(new Vector3(90, 0, Random.Range(0, 360))));

            
        }

    }

    public void FindSomething()
    {
        isAsleep = false;

        ducklingStats.replenishEnergy = false;
        ducklingStats.replenishHunger = false;
        ducklingStats.replenishInterest = false;
        ducklingStats.replenishAffection = false;

        particleSystem.textureSheetAnimation.SetSprite(0, interestSprite);
        var col = particleSystem.colorOverLifetime;
        col.color = interestParticleColour;

        if (!particleSystem.isPlaying)
        {
            particleSystem.Play();
        } 

        if (!destinationSetter.enabled)
        {
            waypointSet = false;
            destinationSetter.enabled = true;
        }

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
        isAsleep = false;

        particleSystem.textureSheetAnimation.SetSprite(0, excitementSprite);
        var col = particleSystem.colorOverLifetime;
        col.color = excitementColour;

        if (!particleSystem.isPlaying)
        {
            particleSystem.Play();
        }

        actionTarget = destinationSetter.target;
        destinationSetter.enabled = false;

        ducklingStats.replenishEnergy = false;
        ducklingStats.replenishHunger = false;
        ducklingStats.replenishInterest = true;
        ducklingStats.replenishAffection = false;

        if (!ducklingBrain.objectDetection.detectedObjects.Contains(actionTarget))
        {
            actionTarget = null;
        }
    }
    
    public void PlayWithPlayer()
    {
        isAsleep = false;

        actionTarget = destinationSetter.target;
        destinationSetter.enabled = true;

        ducklingStats.replenishEnergy = false;
        ducklingStats.replenishHunger = false;
        ducklingStats.replenishInterest = true;
        ducklingStats.replenishAffection = true;

        particleSystem.textureSheetAnimation.SetSprite(0,affectionSprite);
        var col = particleSystem.colorOverLifetime;
        col.color = affectionParticleColour;

        if (!particleSystem.isPlaying)
        {
            particleSystem.Play();
        }

        if (!ducklingBrain.objectDetection.detectedObjects.Contains(actionTarget))
        {
            actionTarget = null;
        }
    }

    public void Sleep()
    {
        destinationSetter.enabled = false;

        ducklingStats.replenishEnergy = true;
        ducklingStats.replenishHunger = false;
        ducklingStats.replenishInterest = false;
        ducklingStats.replenishAffection = false;

        particleSystem.textureSheetAnimation.SetSprite(0, energySprite);
        var col = particleSystem.colorOverLifetime;
        col.color = energyParticleColour;
        
        isAsleep = true;

        SetEyesStatus();

        if (!particleSystem.isPlaying)
        {
            particleSystem.Play();
        }
    }
}
