using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DucklingObjectDetection : MonoBehaviour
{
    public Transform eyes;

    public string[] interestingObjectTags;

    public List<Transform> detectedObjects;
    public Transform interestingObject;

    private DucklingStats ducklingStats;
    private DucklingBrain ducklingBrain;

    public bool objectDetectionActivated;

    public float objectDetectionRefreshTimer;
    public float refreshTimerReset;
    void Start()
    {
        ducklingBrain = GetComponent<DucklingBrain>();
        ducklingStats = ducklingBrain.ducklingStats;

        objectDetectionRefreshTimer = refreshTimerReset;
    }

    public void Update()
    {
        if (objectDetectionActivated)
        {
            DetectObjects();

            objectDetectionRefreshTimer -= Time.deltaTime;

            if (objectDetectionRefreshTimer <= 0f)
            {
                CheckDetectedObjects();
            }
        }
        
    }

    public void DetectObjects()
    {
        RaycastHit[] hits = Physics.SphereCastAll(eyes.position, ducklingStats.lookRadius, transform.forward, 0, 3);


        for (int i = 0; i < hits.Length; i++)
        {
            foreach (string tag in interestingObjectTags)
            {
                if (hits[i].transform.CompareTag(tag) && !detectedObjects.Contains(hits[i].transform))
                {
                    detectedObjects.Add(hits[i].transform);
                }
            }
        }
    }

    public void CheckDetectedObjects()
    {
        RaycastHit[] hits = Physics.SphereCastAll(eyes.position, ducklingStats.lookRadius, transform.forward, 0, 3);

        if (detectedObjects.Count > hits.Length)
        {
            foreach (Transform detectedObject in detectedObjects)
            {
                for (int x = 0; x < hits.Length; x++)
                {
                    if (detectedObject != hits[x].transform)
                    {
                        detectedObjects.Remove(detectedObject);
                    }
                }
            }
        }
        else objectDetectionRefreshTimer = refreshTimerReset;
    }

}
