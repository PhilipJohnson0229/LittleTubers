using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    public GameObject triggeredEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EvenTrigger") 
        {
            triggeredEffect.SetActive(true);

            
        }
    }
}
