using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DucklingStats")]
public class DucklingStats : ScriptableObject
{

    public string ducklingName;

    public float lookRadius;

    public float affection = 100f, affectionTimerDecrease, affectionReplenishRate;

    public float energy = 100f, energyTimerDecrease, energyReplenishRate;

    public float hunger = 100f, hungerTimerDecrease, hungerReplenishRate;

    public float interest = 0f, interestTimerDecrease, interestReplenishRate;

    public bool replenishAffection, replenishEnergy, replenishHunger, replenishInterest;

    public float playerMoney;

    public void AffectionTimer()
    {
        if (!replenishAffection)
        {
            affection -= Time.deltaTime * affectionTimerDecrease;
        }
        else affection += Time.deltaTime * affectionReplenishRate;

    }

    public void EnergyTimer()
    {
        if (!replenishEnergy)
        {
            energy -= Time.deltaTime * energyTimerDecrease;
        }
        else energy += Time.deltaTime * energyReplenishRate;

    }

    public void HungerTimer()
    {
        if (!replenishHunger)
        {
            hunger -= Time.deltaTime * hungerTimerDecrease;
        }
        else hunger += Time.deltaTime * hungerReplenishRate;

    }

    public void InterestTimer()
    {
        if (!replenishInterest)
        {
            interest -= Time.deltaTime * interestTimerDecrease;
        }
        else interest += Time.deltaTime * interestReplenishRate;

    }
}
