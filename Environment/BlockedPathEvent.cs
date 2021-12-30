using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedPathEvent : MonoBehaviour
{
    public bool carriesProp;
    [SerializeField]
    private bool  activated;
    public GameObject propToHold;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EvenTrigger")
        {
            Debug.Log("just hit " + other.name);
            Animator eventTriggerAnim = other.GetComponent<Animator>();
            eventTriggerAnim.SetBool("Activate", true);
        }

        if (other.tag == "Player")
        {
            if (!activated) 
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    player.canDropEventItem = true;
                    player.SetCurrentObstacle(this);
                    if (player.carryingEventItem)
                    {
                        UIManager.instance.Notification("Press E To Interact");
                    }
                }
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
                player.canDropEventItem = false;

                player.SetCurrentObstacle(null);
            }
        }
    }

    public void ActivateProp()
    {
        if (propToHold != null) 
        {
            propToHold.SetActive(true);
        }
        activated = true;
    }
}
