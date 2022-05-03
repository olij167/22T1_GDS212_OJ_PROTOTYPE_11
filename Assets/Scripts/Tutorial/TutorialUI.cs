using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "TutotrialUI")]
public class TutorialUI : ScriptableObject
{
    //public Sprite tutorialIcon;
    public string tutorialInstructions;
    public bool tutorialStepComplete;

    //public AudioClip voiceOver;
    public float tutorialSlideDuration;
    //public Camera;

    public List<KeyCode> tutorialInputList;
    public string inputString;
}
