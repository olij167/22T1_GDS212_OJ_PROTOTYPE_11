using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "TimeSave")]
public class TimeSO : ScriptableObject
{
    public float timeHours, timeMinutes, timeSeconds;

    public string currentDay;

    public bool isLoaded;


}
