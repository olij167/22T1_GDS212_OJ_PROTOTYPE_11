using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class DucklingStats : MonoBehaviour
{
    //public BehaviorTree behaviorTree;

    public float affection = 100f, affectionTimerDecrease, affectionReplenishRate;

    public float energy = 100f, energyTimerDecrease, energyReplenishRate;

    public float hunger = 100f, hungerTimerDecrease, hungerReplenishRate;

    public float interest = 0f, interestTimerDecrease, interestReplenishRate;

    //public float energyTimerReset, hungerTimerReset, cleanlinessTimerReset, affectionTimerReset;

    private void Start()
    {
        
    }

    void Update()
    {
        AffectionTimer();

        EnergyTimer();

        HungerTimer();

        CleanlinessTimer();
    }

    //public string GetLowestStat(string lowestStatName)
    //{
    //    float lowestStat = Mathf.Min(affection, energy, hunger, interest);

    //    if (lowestStat == affection)
    //    {
    //        return lowestStatName = "affection";
    //    }

    //    if (lowestStat == energy)
    //    {
    //        return lowestStatName = "energy";
    //    }

    //    if (lowestStat == hunger)
    //    {
    //        return lowestStatName = "hunger";
    //    }

    //    if (lowestStat == interest)
    //    {
    //        return lowestStatName = "interest";
    //    }


    //}

    void AffectionTimer()
    {
        affection -= Time.deltaTime * affectionTimerDecrease;
        //behaviorTree.SetVariable("affection", (SharedFloat)affection);

        if (affection <= 0f)
        {
            //activate neglected behaviour
        }
    }

    void EnergyTimer()
    {
        energy -= Time.deltaTime * energyTimerDecrease;
        //behaviorTree.SetVariable("energy", (SharedFloat)energy);


        if (energy <= 0f)
        {
            //activate sleep behaviour
        }
    }

    void HungerTimer()
    {
        hunger -= Time.deltaTime * hungerTimerDecrease;
        //behaviorTree.SetVariable("hunger", (SharedFloat)hunger);


        if (hunger <= 0f)
        {
            // activate hungry behaviour
        }
    }

    void CleanlinessTimer()
    {
        interest += Time.deltaTime * interestTimerDecrease;
        //behaviorTree.SetVariable("cleanliness", (SharedFloat)cleanliness);


        if (interest <= 0f)
        {
            // activate bored behaviour
        }
    }
}
