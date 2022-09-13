using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            Player player = other.GetComponent<Player>();

            if (player != null) 
            {
                player.PickUpKey();

                gameObject.SetActive(false);
            }
        }
    }
}
