using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedPathEvent : MonoBehaviour
{
    public bool carriesProp, isComponent;
    [SerializeField]
    private bool  activated;
    public GameObject[] propToHold;

    public int[] soundToPlay;

    [SerializeField]
    private int soundIndex = 0;

    public Event eventToTrigger;
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
                    player.setCanDropEventItem(true);
                    player.SetCurrentObstacle(this);
                    if (player.getCarryingEventItem() == true)
                    {
                        UIManager uiManager = GameObject.FindObjectOfType<UIManager>();
                        if (uiManager != null) 
                        {
                            uiManager.Notification("Press E To Interact");
                        }
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
                player.setCanDropEventItem(false);

                player.SetCurrentObstacle(null);
            }
        }
    }

    public void ActivateProp()
    {
        for (int i = 0; i < propToHold.Length; i++) 
        {
            if (propToHold[i] != null)
            {
                propToHold[i].SetActive(true);
            }
           
        }
        activated = true;

        if (isComponent) 
        {
            eventToTrigger.TriggerEvent();
        }
    }

    public void playSounds() 
    {
        AudioManager.instance.PlaySoundEffects(soundToPlay[soundIndex]);
        soundIndex++;
    }
}
