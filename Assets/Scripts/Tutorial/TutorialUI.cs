using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "TutotrialUI")]
public class TutorialUI : ScriptableObject
{
    public Sprite tutorialIcon;
    public string tutorialInstructions;
    public bool tutorialStepComplete, isLookAroundTutorial;

    public AudioClip voiceOver;

    public List<KeyCode> tutorialInputList;
}
