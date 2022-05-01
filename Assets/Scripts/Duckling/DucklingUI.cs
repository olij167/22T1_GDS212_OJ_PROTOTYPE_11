using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DucklingUI : MonoBehaviour
{
    public Slider affectionBar, energyBar, hungerBar, interestBar;

    public TextMeshProUGUI ducklingNameText;

    public DucklingStats ducklingStats;

    // Start is called before the first frame update
    void Start()
    {
        ducklingNameText.text = ducklingStats.ducklingName;
    }

    // Update is called once per frame
    void Update()
    {
        affectionBar.value = ducklingStats.affection;
        energyBar.value = ducklingStats.energy;
        hungerBar.value = ducklingStats.hunger;
        interestBar.value = ducklingStats.interest;
    }
}
