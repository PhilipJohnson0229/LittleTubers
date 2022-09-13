using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPockets : MonoBehaviour
{
    public GameObject burnHeathensEffect;
    public Animator curtainAnim;
    public GameObject blamoBanisher;

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            if (curtainAnim != null) 
            {
                curtainAnim.SetBool("Activate", true);
            }

            if (blamoBanisher != null) 
            {
                blamoBanisher.SetActive(true);
            }
           
            Player player = other.GetComponent<Player>();

            if (player != null && player.getCarryingEventItem())
            {
                player.dropEventItem();
                player.setCarryingEventItem(false);
            }

            Berserker[] enemiesRemaining = GameObject.FindObjectsOfType<Berserker>();
            BirdActivator[] potentialBeacons = GameObject.FindObjectsOfType<BirdActivator>();

            for (int i = 0; i < enemiesRemaining.Length; i++)
            {
                enemiesRemaining[i].SetTarget(potentialBeacons[0].transform);
            }
        }

        if (other.tag == "EventItem") 
        {
           
            Instantiate(burnHeathensEffect, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                Berserker[] enemiesRemaining = GameObject.FindObjectsOfType<Berserker>();
                

                for (int i = 0; i < enemiesRemaining.Length; i++)
                {
                    enemiesRemaining[i].SetTarget(player.transform);
                }
            }
        }
    }
}
