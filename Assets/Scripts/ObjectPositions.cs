using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Object Position Tracker")]
public class ObjectPositions : ScriptableObject
{
    public List<float[]> foodPositions, toyPositions;

    public bool isLoaded;

    public void GetFoodPositions()
    {
        foodPositions.Clear();

        foreach (GameObject food in GameObject.FindGameObjectsWithTag("Food"))
        {
            float[] position = new float[3];
            position[0] = food.transform.position.x;
            position[1] = food.transform.position.y;
            position[2] = food.transform.position.z;

            foodPositions.Add(position);
        }
    }
    
    public void GetToyPositions()
    {
        toyPositions.Clear();

        foreach (GameObject toy in GameObject.FindGameObjectsWithTag("InterestingObject"))
        {
            float[] position = new float[3];
            position[0] = toy.transform.position.x;
            position[1] = toy.transform.position.y;
            position[2] = toy.transform.position.z;

            toyPositions.Add(position);
        }
    }
}
