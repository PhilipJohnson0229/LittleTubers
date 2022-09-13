using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLock : MonoBehaviour
{
    public GameObject lockedDoor, lockVisual;
    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            Player player = other.GetComponent<Player>();

            if (player != null && player.playerData.isHasKey() == true) 
            {
                player.UseKey();

                if (lockedDoor != null)
                {
                    lockVisual.SetActive(false);

                    Animator doorAnim = lockedDoor.GetComponent<Animator>();

                    doorAnim.SetBool("Open", true);                 
                }               
            }
        }
        
    }
}
