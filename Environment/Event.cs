using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    public GameObject triggeredEffect;

    public GameObject itemToTurnOff;

    public GameObject[] keys;

    public int requiredKeys;


    private int heldKeys;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EvenTrigger") 
        {
            triggeredEffect.SetActive(true);

            if (itemToTurnOff != null) 
            {
                itemToTurnOff.SetActive(false);
            }
        }
    }

    public void TriggerEvent() 
    {
        for (int i = 0; i < keys.Length; i++) 
        {
            if (keys[i].GetComponent<BlockedPathEvent>().isComponent) 
            {
                heldKeys++;
            }
        }

        if (heldKeys >= requiredKeys) 
        {
            triggeredEffect.SetActive(true);
        }
    }
}
