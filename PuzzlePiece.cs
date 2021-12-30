using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null) 
            {
                player.canPickUpEventItem = true;

                player.currentKnifer = this.gameObject;

                UIManager.instance.Notification("Press E to pick up the scumbag");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.canPickUpEventItem = false;
            }
        }
    }
}
