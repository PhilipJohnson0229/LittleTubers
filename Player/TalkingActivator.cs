using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingActivator : MonoBehaviour
{
    public int animVoiceIndex;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") 
        {
            Player player = other.GetComponent<Player>();

            if (player != null) 
            {
                player.speak(animVoiceIndex);
            }
        }
    }
}
