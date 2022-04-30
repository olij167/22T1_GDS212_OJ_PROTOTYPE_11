using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DucklingStats : MonoBehaviour
{
    //public BehaviorTree behaviorTree;

    public Slider affectionBar, energyBar, hungerBar, interestBar;

    public float lookRadius;

    public float affection = 100f, affectionTimerDecrease, affectionReplenishRate;

    public float energy = 100f, energyTimerDecrease, energyReplenishRate;

    public float hunger = 100f, hungerTimerDecrease, hungerReplenishRate;

    public float interest = 0f, interestTimerDecrease, interestReplenishRate;

    public bool replenishAffection, replenishEnergy, replenishHunger, replenishInterest;

    //public float energyTimerReset, hungerTimerReset, cleanlinessTimerReset, affectionTimerReset;

    private void Start()
    {
        
    }

    void Update()
    {
        
        AffectionTimer();
        affection = Mathf.Clamp(affection, 0f, 100f);

        EnergyTimer();
        energy = Mathf.Clamp(energy, 0f, 100f);

        HungerTimer();
        hunger = Mathf.Clamp(hunger, 0f, 100f);

        InterestTimer();
        interest = Mathf.Clamp(interest, 0f, 100f);

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
        if (!replenishAffection)
        {
            affection -= Time.deltaTime * affectionTimerDecrease;
        }
        else affection += Time.deltaTime * affectionReplenishRate;

        affectionBar.value = affection;
    }

    void EnergyTimer()
    {
        if (!replenishEnergy)
        {
            energy -= Time.deltaTime * energyTimerDecrease;
        }
        else energy += Time.deltaTime * energyReplenishRate;

        energyBar.value = energy;
    }

    void HungerTimer()
    {
        if (!replenishHunger)
        {
            hunger -= Time.deltaTime * hungerTimerDecrease;
        }
        else hunger += Time.deltaTime * hungerReplenishRate;

        hungerBar.value = hunger; ;
    }

    void InterestTimer()
    {
        if (!replenishInterest)
        {
            interest -= Time.deltaTime * interestTimerDecrease;
        }
        else interest += Time.deltaTime * interestReplenishRate;

        interestBar.value = interest;
    }
}
