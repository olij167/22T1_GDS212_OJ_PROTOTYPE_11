using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Object Position Tracker")]
public class ObjectPositions : ScriptableObject
{
    public List<Vector3> foodPositions, toyPositions;

    public void GetFoodPositions()
    {
        foodPositions.Clear();

        foreach (GameObject food in GameObject.FindGameObjectsWithTag("Food"))
        {
            foodPositions.Add(food.transform.position);
        }
    }
    
    public void GetToyPositions()
    {
        toyPositions.Clear();

        foreach (GameObject toy in GameObject.FindGameObjectsWithTag("InterestingObject"))
        {
            toyPositions.Add(toy.transform.position);
        }
    }
}
