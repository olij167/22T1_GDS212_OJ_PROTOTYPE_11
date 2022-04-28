using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeTrigger : MonoBehaviour
{
    public AudioSource narratorAudioSource;

    public List<AudioClip> narrativeAudioList;

    int count;

    bool narrativePlayed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NarrativeTrigger"))
        {
            //if (narratorAudioSource.isPlaying)
            //{
            //    narratorAudioSource.Stop();
            //}

            if (!narratorAudioSource.isPlaying)
            { 

                narratorAudioSource.PlayOneShot(narrativeAudioList[count]);

                narrativePlayed = true;

                
            }

            if (count + 1 <= narrativeAudioList.Count && narrativePlayed)
            {
                count++;
                narrativePlayed = false;
            }
            else
            {
                //narrativePlayed = false;
                //GetComponent<Collider>().enabled = false;
            }
        }
    }
}
