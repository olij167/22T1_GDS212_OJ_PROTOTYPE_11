using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float affection, energy, hunger, interest, currentHours, currentMinutes, currentSeconds, playerMoney;

    public string ducklingName, currentDay;

    //public List<float[]> toyPositionList, foodPositionList;

    //public ObjectPositions objectPositions;

    public SaveData (DucklingStats ducklingStats, TimeSO time)
    {
        affection = ducklingStats.affection;
        energy = ducklingStats.energy;
        hunger = ducklingStats.hunger;
        interest = ducklingStats.interest;
        playerMoney = ducklingStats.playerMoney;

        currentDay = time.currentDay;
        currentHours = time.timeHours;
        currentMinutes = time.timeMinutes;
        currentSeconds = time.timeSeconds;

        

        //toyPositionList = new List<float[]>();
        //foodPositionList = new List<float[]>();

        //foreach (float[] food in objectPositions.foodPositions)
        //{

        //    //float[] position;
        //    //position = new float[3];
        //    //position[0] = food.x;
        //    //position[1] = food.y;
        //    //position[2] = food.z;

        //    foodPositionList.Add(food);
            
        //}

        //foreach (float[] toy in objectPositions.toyPositions)
        //{
        //    //float[] position;
        //    //position = new float[3];
        //    //position[0] = toy.x;
        //    //position[1] = toy.y;
        //    //position[2] = toy.z;

        //    toyPositionList.Add(position);

        //}
    }
}
