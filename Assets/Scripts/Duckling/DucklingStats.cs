using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DucklingStats : MonoBehaviour
{
    public float affection = 100f, affectionTimerDecrease;

    public float energy = 100f, energyTimerDecrease;

    public float hunger = 100f, hungerTimerDecrease;

    public float cleanliness = 100f, cleanlinessTimerDecrease;

    //public float energyTimerReset, hungerTimerReset, cleanlinessTimerReset, affectionTimerReset;

    void Update()
    {
        AffectionTimer();

        EnergyTimer();

        HungerTimer();

        CleanlinessTimer();
    }

    void AffectionTimer()
    {
        affection -= Time.deltaTime * affectionTimerDecrease;

        if (affection <= 0f)
        {
            //activate neglected behaviour
        }
    }

    void EnergyTimer()
    {
        energy -= Time.deltaTime * energyTimerDecrease;

        if (energy <= 0f)
        {
            //activate sleep behaviour
        }
    }

    void HungerTimer()
    {
        hunger -= Time.deltaTime * hungerTimerDecrease;

        if (hunger <= 0f)
        {
            // activate hungry behaviour
        }
    }

    void CleanlinessTimer()
    {
        cleanliness -= Time.deltaTime * cleanlinessTimerDecrease;

        if (cleanliness <= 0f)
        {
            // activate stinky behaviour
        }
    }
}
