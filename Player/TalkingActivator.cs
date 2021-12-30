using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingActivator : MonoBehaviour
{
   
    public int animVoiceIndex;
    public bool useRandom;
    public int randMin, randMax;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") 
        {
            Player player = other.GetComponent<Player>();

            if (player != null) 
            {
                if (useRandom) 
                {
                    animVoiceIndex = (Random.Range(randMin, randMax));
                    player.speak(animVoiceIndex);
                   
                    Destroy(gameObject);
                }
                else 
                {
                    player.speak(animVoiceIndex);
                   
                    Destroy(gameObject);
                }
                
            }
        }
    }

   
}
