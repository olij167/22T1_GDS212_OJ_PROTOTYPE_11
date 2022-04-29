using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DucklingObjectDetection : MonoBehaviour
{
    public string[] interestingObjectTags;

    public List<Transform> detectedObjects;
    public Transform interestingObject;
    void Start()
    {
        
    }

    public void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            for (int i = 0; i < interestingObjectTags.Length; i++)
            {
                if (hit.transform.CompareTag(interestingObjectTags[i]))
                {
                    detectedObjects.Add(hit.transform);
                }

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform != detectedObjects[i])
                    {
                        detectedObjects.Remove(detectedObjects[i]);
                    }
                }
            }
        }
    }

}
