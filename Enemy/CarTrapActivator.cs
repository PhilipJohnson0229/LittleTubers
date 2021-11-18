using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivator : MonoBehaviour
{
    public bool activator;

    public GameObject trapToActivate;

    public GameObject[] sisterObjects;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            if (activator)
            {
                trapToActivate.SetActive(true);
                sisterObjects[0].SetActive(true);
                this.gameObject.SetActive(false);
            }
            else 
            {
                trapToActivate.SetActive(false);
                sisterObjects[0].SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }
}
