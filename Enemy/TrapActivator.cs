using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrapActivator : MonoBehaviour
{
    public bool activator;

    public GameObject carManager;

    public GameObject sisterObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            if (activator)
            {
                carManager.SetActive(true);
                sisterObject.SetActive(true);
            }
        }
    }
}
